using SolrNetCore.Commands.Parameters;
using SolrNetCore.Impl;

namespace SolrNetCore
{
    /// <summary>
    /// Executable query
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public interface ISolrQueryExecuter<T>
    {
        /// <summary>
        /// Executes the query and returns results
        /// </summary>
        /// <returns>query results</returns>
        SolrQueryResults<T> Execute(ISolrQuery q, QueryOptions options);

        SolrMoreLikeThisHandlerResults<T> Execute(SolrMLTQuery query, MoreLikeThisHandlerQueryOptions options);
    }
}