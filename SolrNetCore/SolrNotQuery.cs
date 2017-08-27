
namespace SolrNetCore
{
    /// <summary>
    /// Negates a query
    /// </summary>
    public class SolrNotQuery : AbstractSolrQuery
    {
        private readonly ISolrQuery query;

        /// <summary>
        /// Negates a query
        /// </summary>
        /// <param name="q"></param>
        public SolrNotQuery(ISolrQuery q)
        {
            query = q;
        }

        public ISolrQuery Query
        {
            get { return query; }
        }
    }
}