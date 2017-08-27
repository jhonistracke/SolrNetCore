﻿using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace SolrNetCore.Commands
{
    /// <summary>
    /// Adds / updates documents to solr
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public class AddCommand<T> : ISolrCommand
    {
        private readonly IEnumerable<KeyValuePair<T, double?>> documents = new List<KeyValuePair<T, double?>>();
        private readonly ISolrDocumentSerializer<T> documentSerializer;
        private readonly AddParameters parameters;

        /// <summary>
        /// Adds / updates documents to solr
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="serializer"></param>
        /// <param name="parameters"></param>
	    public AddCommand(IEnumerable<KeyValuePair<T, double?>> documents, ISolrDocumentSerializer<T> serializer, AddParameters parameters)
        {
            this.documents = documents;
            documentSerializer = serializer;
            this.parameters = parameters;
        }

        /// <summary>
        /// Removes UTF control characters, not valid in XML
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        /// <seealso href="http://cse-mjmcl.cse.bris.ac.uk/blog/2007/02/14/1171465494443.html#comment1221120563572"/>
        /// <summary>
        /// Serializes command to Solr XML
        /// </summary>
        /// <returns></returns>
        public string ConvertToXml()
        {
            var addElement = new XElement("add");
            if (parameters != null)
            {
                if (parameters.CommitWithin.HasValue)
                {
                    var commit = new XAttribute("commitWithin", parameters.CommitWithin.Value.ToString(CultureInfo.InvariantCulture));
                    addElement.Add(commit);
                }
                if (parameters.Overwrite.HasValue)
                {
                    var overwrite = new XAttribute("overwrite", parameters.Overwrite.Value.ToString().ToLowerInvariant());
                    addElement.Add(overwrite);
                }
            }
            foreach (var docWithBoost in documents)
            {
                var xmlDoc = documentSerializer.Serialize(docWithBoost.Key, docWithBoost.Value);
                addElement.Add(xmlDoc);
            }
            return addElement.ToString(SaveOptions.DisableFormatting);
        }

        public string Execute(ISolrConnection connection)
        {
            var xml = ConvertToXml();
            return connection.Post("/update", xml);
        }
    }
}