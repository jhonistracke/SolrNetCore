
using System;
using System.Xml.Linq;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Parses a single Solr XML result node
    /// </summary>
    public interface ISolrFieldParser {
        /// <summary>
        /// True if this parser can handle the solrType (int, bool, str, ...)
        /// </summary>
        /// <param name="solrType"></param>
        /// <returns></returns>
        bool CanHandleSolrType(string solrType);

        /// <summary>
        /// True if this parser can handle a type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool CanHandleType(Type t);

        /// <summary>
        /// Parses a single Solr XML result node
        /// </summary>
        /// <param name="field">Solr XML result node</param>
        /// <param name="t">Type the node value should be converted to</param>
        /// <returns>Parsed value</returns>
        object Parse(XElement field, Type t);
    }
}