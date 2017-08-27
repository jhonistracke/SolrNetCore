using System.Xml.Linq;

namespace SolrNetCore.Impl.DocumentPropertyVisitors
{
    /// <summary>
    /// Default document visitor
    /// </summary>
    public class DefaultDocumentVisitor : ISolrDocumentPropertyVisitor
    {
        private readonly AggregateDocumentVisitor visitor;

        /// <summary>
        /// Default document visitor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="parser"></param>
        public DefaultDocumentVisitor(IReadOnlyMappingManager mapper, ISolrFieldParser parser)
        {
            visitor = new AggregateDocumentVisitor(new ISolrDocumentPropertyVisitor[] {
                new RegularDocumentVisitor(parser, mapper),
                new GenericDictionaryDocumentVisitor(mapper, parser),
            });
        }

        public void Visit(object doc, string fieldName, XElement field)
        {
            visitor.Visit(doc, fieldName, field);
        }
    }
}