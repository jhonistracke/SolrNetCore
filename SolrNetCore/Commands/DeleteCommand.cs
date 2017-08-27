using SolrNetCore.Commands.Parameters;
using SolrNetCore.Utils;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SolrNetCore.Commands
{
    /// <summary>
    /// Deletes document(s), either by id or by query
    /// </summary>
	public class DeleteCommand : ISolrCommand
    {
        private readonly DeleteByIdAndOrQueryParam deleteParam;
        private readonly DeleteParameters parameters;

        public DeleteCommand(DeleteByIdAndOrQueryParam deleteParam, DeleteParameters parameters)
        {
            this.deleteParam = deleteParam;
            this.parameters = parameters;
        }

        /// <summary>
        /// Deprecated in Solr 1.3
        /// </summary>
		public bool? FromPending { get; set; }

        /// <summary>
        /// Deprecated in Solr 1.3
        /// </summary>
		public bool? FromCommitted { get; set; }

        public string Execute(ISolrConnection connection)
        {
            var deleteNode = new XElement("delete");
            if (parameters != null)
            {
                if (parameters.CommitWithin.HasValue)
                {
                    var attr = new XAttribute("commitWithin", parameters.CommitWithin.Value.ToString(CultureInfo.InvariantCulture));
                    deleteNode.Add(attr);
                }
            }
            var param = new[] {
                KV.Create(FromPending, "fromPending"),
                KV.Create(FromCommitted, "fromCommitted")
            };
            foreach (var p in param)
            {
                if (p.Key.HasValue)
                {
                    var att = new XAttribute(p.Value, p.Key.Value.ToString().ToLower());
                    deleteNode.Add(att);
                }
            }
            deleteNode.Add(deleteParam.ToXmlNode().ToArray());
            return connection.Post("/update", deleteNode.ToString(SaveOptions.DisableFormatting));
        }
    }
}