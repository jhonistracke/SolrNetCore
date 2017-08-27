namespace SolrNetCore
{
    /// <summary>
    /// Requires a query
    /// </summary>
    public class SolrRequiredQuery : AbstractSolrQuery
    {
        private readonly ISolrQuery query;

        /// <summary>
        /// Requires a query
        /// </summary>
        /// <param name="q"></param>
        public SolrRequiredQuery(ISolrQuery q)
        {
            query = q;
        }

        public ISolrQuery Query
        {
            get { return query; }
        }
    }
}
