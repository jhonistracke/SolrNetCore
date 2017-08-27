using SolrNetCore.Commands.Parameters;
using System.Collections.Generic;

namespace SolrNetCore
{
    /// <summary>
    /// Read-only operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISolrReadOnlyOperations<T> : ISolrBasicReadOnlyOperations<T>
    {
        /// <summary>
        /// Executes a query
        /// </summary>
        /// <param name="q">query to execute</param>
        /// <returns>query results</returns>
        SolrQueryResults<T> Query(string q);

        /// <summary>
        /// Executes a query
        /// </summary>
        /// <param name="q"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        SolrQueryResults<T> Query(string q, ICollection<SortOrder> orders);

        /// <summary>
        /// Executes a query
        /// </summary>
        /// <param name="q"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        SolrQueryResults<T> Query(string q, QueryOptions options);

        /// <summary>
        /// Executes a query
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        SolrQueryResults<T> Query(ISolrQuery q);

        /// <summary>
        /// Executes a query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        SolrQueryResults<T> Query(ISolrQuery query, ICollection<SortOrder> orders);

        /// <summary>
        /// Executes a single facet field query
        /// </summary>
        /// <param name="facets"></param>
        /// <returns></returns>
        ICollection<KeyValuePair<string, int>> FacetFieldQuery(SolrFacetFieldQuery facets);
    }
}