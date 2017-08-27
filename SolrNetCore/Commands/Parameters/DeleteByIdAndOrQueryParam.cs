using SolrNetCore.Impl;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SolrNetCore.Commands.Parameters
{
    /// <summary>
    /// Parameter to delete document(s) by one or more ids
    /// and or a query parameters.
    /// </summary>
    public class DeleteByIdAndOrQueryParam
    {
        private readonly IEnumerable<string> ids;
        private readonly ISolrQuery query;
        private readonly ISolrQuerySerializer querySerializer;

        public DeleteByIdAndOrQueryParam(IEnumerable<string> ids, ISolrQuery query, ISolrQuerySerializer querySerializer)
        {
            this.ids = ids;
            this.query = query;
            this.querySerializer = querySerializer;
        }

        public IEnumerable<XElement> ToXmlNode()
        {
            if (ids != null)
                foreach (var i in ids)
                    yield return new XElement("id", i);
            if (query != null)
            {
                var value = querySerializer.Serialize(query);
                yield return new XElement("query", value);
            }
        }
    }
}