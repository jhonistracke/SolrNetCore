using System.Collections.Generic;
using System.IO;

namespace SolrNetCore
{
    /// <summary>
    /// Manages HTTP connection with Solr
    /// </summary>
    public interface ISolrConnection
    {
        /// <summary>
        /// POSTs to Solr
        /// </summary>
        /// <param name="relativeUrl">Path to post to</param>
        /// <param name="s">POST content</param>
        /// <returns></returns>
        string Post(string relativeUrl, string s);

        /// <summary>
        /// POSTs binary to Solr
        /// </summary>
        /// <param name="relativeUrl">Path to post to</param>
        /// <param name="contentType">Request content type (optional)</param>
        /// <param name="content">Binary content</param>
        /// <param name="getParameters">extra parameters to pass in query string</param>
        /// <returns></returns>
        string PostStream(string relativeUrl, string contentType, Stream content, IEnumerable<KeyValuePair<string, string>> getParameters);

        /// <summary>
        /// GETs from Solr
        /// </summary>
        /// <param name="relativeUrl">Path to get from</param>
        /// <param name="parameters">Query string parameters</param>
        /// <returns></returns>
        string Get(string relativeUrl, IEnumerable<KeyValuePair<string, string>> parameters);
    }
}