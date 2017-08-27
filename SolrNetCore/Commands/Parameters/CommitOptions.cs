namespace SolrNetCore.Commands.Parameters
{
    /// <summary>
    /// Commit/optimize options
    /// </summary>
	public class CommitOptions
    {
        /// <summary>
        /// Block until a new searcher is opened and registered as the main query searcher, making the changes visible. 
        /// Default is true
        /// </summary>
		public bool? WaitSearcher { get; set; }

        /// <summary>
        /// Block until index changes are flushed to disk
        /// Default is true
        /// </summary>
		public bool? WaitFlush { get; set; }

        /// <summary>
        /// Optimizes down to at most this number of segments
        /// Default is 1
        /// </summary>
        /// <remarks>Requires Solr 1.3</remarks>
        public int? MaxSegments { get; set; }

        /// <summary>
        /// Merge segments with deletes away
        /// </summary>
        /// <remarks>Requires Solr 1.4</remarks>
        public bool? ExpungeDeletes { get; set; }
    }
}