
namespace SolrNetCore.Impl {
    /// <summary>
    /// HTTP-level cache for Solr responses
    /// </summary>
    public interface ISolrCache {
        /// <summary>
        /// Gets a cached Solr response. Returns null if there is no cached response for this URL
        /// </summary>
        /// <param name="url">Full Solr query URL (including all querystring parameters)</param>
        /// <returns></returns>
        SolrCacheEntity this[string url] { get; }

        /// <summary>
        /// Adds a Solr response to the cache
        /// </summary>
        /// <param name="e"></param>
        void Add(SolrCacheEntity e);
    }
}