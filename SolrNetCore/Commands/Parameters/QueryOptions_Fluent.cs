using System.Collections.Generic;

namespace SolrNetCore.Commands.Parameters
{
    public partial class QueryOptions
    {
        private static List<T> Add<T>(IEnumerable<T> l, IEnumerable<T> l2)
        {
            var list = new List<T>(l);
            list.AddRange(l2);
            return list;
        }

        /// <summary>
        /// Adds selected fields to this instance
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public QueryOptions AddFields(params string[] fields)
        {
            Fields = Add(Fields, fields);
            return this;
        }

        /// <summary>
        /// Adds sort orders to this instance
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public QueryOptions AddOrder(params SortOrder[] order)
        {
            OrderBy = Add(OrderBy, order);
            return this;
        }

        /// <summary>
        /// Adds filter queries to this instance
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        public QueryOptions AddFilterQueries(params ISolrQuery[] queries)
        {
            FilterQueries = Add(FilterQueries, queries);
            return this;
        }

        /// <summary>
        /// Adds facet queries to this instance
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        public QueryOptions AddFacets(params ISolrFacetQuery[] queries)
        {
            Facet.Queries = Add(Facet.Queries, queries);
            return this;
        }
    }
}