
using System;

namespace SolrNetCore.Impl {
    /// <summary>
    /// HTTP-level Solr cache entry
    /// </summary>
    [Serializable]
    public class SolrCacheEntity {
        /// <summary>
        /// Full Solr query URL
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Response ETag
        /// </summary>
        public string ETag { get; private set; }

        /// <summary>
        /// Response data
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// HTTP-level Solr cache entry
        /// </summary>
        /// <param name="url">Full Solr query URL</param>
        /// <param name="eTag">Response ETag</param>
        /// <param name="data">Response data</param>
        public SolrCacheEntity(string url, string eTag, string data) {
            Url = url;
            ETag = eTag;
            Data = data;
        }
    }
}