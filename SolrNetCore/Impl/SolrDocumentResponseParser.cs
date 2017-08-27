
using System.Collections.Generic;
using System.Xml.Linq;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Parses documents from query response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public class SolrDocumentResponseParser<T> : ISolrDocumentResponseParser<T> {
        private readonly IReadOnlyMappingManager mappingManager;
        private readonly ISolrDocumentPropertyVisitor propVisitor;
        private readonly ISolrDocumentActivator<T> activator;

        public SolrDocumentResponseParser(IReadOnlyMappingManager mappingManager, ISolrDocumentPropertyVisitor propVisitor, ISolrDocumentActivator<T> activator) {
            this.mappingManager = mappingManager;
            this.propVisitor = propVisitor;
            this.activator = activator;
        }

        /// <summary>
        /// Parses documents results
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public IList<T> ParseResults(XElement parentNode) {
            var results = new List<T>();
            if (parentNode == null)
                return results;
            var nodes = parentNode.Elements("doc");
            foreach (var docNode in nodes) {
                results.Add(ParseDocument(docNode));
            }
            return results;
        }

        /// <summary>
        /// Builds a document from the corresponding response xml node
        /// </summary>
        /// <param name="node">response xml node</param>
        /// <param name="fields">document fields</param>
        /// <returns>populated document</returns>
        public T ParseDocument(XElement node) {
            var doc = activator.Create();
            foreach (var field in node.Elements()) {
                string fieldName = field.Attribute("name").Value;
                propVisitor.Visit(doc, fieldName, field);
            }
            return doc;
        }
    }
}