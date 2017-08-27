namespace SolrNetCore
{
    /// <summary>
    /// Arbitrary facet query
    /// </summary>
	public class SolrFacetQuery : ISolrFacetQuery
    {
        private readonly ISolrQuery query;

        public SolrFacetQuery(ISolrQuery q)
        {
            query = q;
        }

        public ISolrQuery Query
        {
            get { return query; }
        }
    }
}