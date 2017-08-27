
using System.Xml.Linq;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Parses a chunk of a query response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public interface ISolrResponseParser<T> : ISolrAbstractResponseParser<T> {
        /// <summary>
        /// Parses a chunk of a query response into the results object
        /// </summary>
        /// <param name="xml">query response</param>
        /// <param name="results">results object</param>
        void Parse(XDocument xml, SolrQueryResults<T> results);
    }
}