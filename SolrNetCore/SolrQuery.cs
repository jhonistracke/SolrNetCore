using SolrNetCore.Impl;

namespace SolrNetCore
{
    /// <summary>
    /// Basic solr query
    /// </summary>	
    public class SolrQuery : AbstractSolrQuery, ISelfSerializingQuery
    {
        private readonly string query;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="query">solr query to execute</param>
        public SolrQuery(string query)
        {
            this.query = query;
        }

        /// <summary>
        /// query to execute
        /// </summary>
        public string Query
        {
            get { return query; }
        }

        /// <summary>
        /// Represents a query for all documents ("*:*")
        /// </summary>
        public static readonly AbstractSolrQuery All = new SolrQuery("*:*");
    }
}