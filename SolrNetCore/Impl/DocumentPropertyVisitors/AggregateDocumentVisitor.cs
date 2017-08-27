using System.Collections.Generic;
using System.Xml.Linq;

namespace SolrNetCore.Impl.DocumentPropertyVisitors
{
    /// <summary>
    /// Aggregate document visitor
    /// </summary>
    public class AggregateDocumentVisitor : ISolrDocumentPropertyVisitor
    {
        private readonly IEnumerable<ISolrDocumentPropertyVisitor> visitors;

        /// <summary>
        /// Aggregate document visitor
        /// </summary>
        /// <param name="visitors">Visitors to aggregate</param>
        public AggregateDocumentVisitor(IEnumerable<ISolrDocumentPropertyVisitor> visitors)
        {
            this.visitors = visitors;
        }

        public void Visit(object doc, string fieldName, XElement field)
        {
            foreach (var v in visitors)
            {
                v.Visit(doc, fieldName, field);
            }
        }
    }
}