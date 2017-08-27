
using System;
using System.Xml.Linq;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Visits a document with a raw xml Solr response field
    /// </summary>
    public interface ISolrDocumentPropertyVisitor {
        /// <summary>
        /// Visits a document with a raw xml Solr response field
        /// </summary>
        /// <param name="doc">Document object</param>
        /// <param name="fieldName">Solr field name</param>
        /// <param name="field">Raw XML Solr field</param>
        void Visit(object doc, string fieldName, XElement field);
    }
}