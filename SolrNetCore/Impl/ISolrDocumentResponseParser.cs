
using System.Collections.Generic;
using System.Xml.Linq;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Parses documents from query response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public interface ISolrDocumentResponseParser<T> {
        /// <summary>
        /// Parses documents from query response
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        IList<T> ParseResults(XElement parentNode);
    }
}