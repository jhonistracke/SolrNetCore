using SolrNetCore.Commands.Parameters;
using SolrNetCore.Impl;
using SolrNetCore.Schema;
using System.Collections.Generic;

namespace SolrNetCore
{
    /// <summary>
    /// Read-only operations without convenience overloads
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISolrBasicReadOnlyOperations<T>
    {
        /// <summary>
        /// Executes a query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        SolrQueryResults<T> Query(ISolrQuery query, QueryOptions options);

        /// <summary>
        /// Executes a MoreLikeThisHandler query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        SolrMoreLikeThisHandlerResults<T> MoreLikeThis(SolrMLTQuery query, MoreLikeThisHandlerQueryOptions options);

        /// <summary>
        /// Pings the Solr server.
        /// It can be used by a load balancer in front of a set of Solr servers to check response time of all the Solr servers in order to do response time based load balancing.
        /// See http://wiki.apache.org/solr/SolrConfigXml for more information.
        /// </summary>
        ResponseHeader Ping();

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <param name="schemaFileName">Name of the schema file.</param>
        /// <returns> Solr schema </returns>
        SolrSchema GetSchema(string schemaFileName);

        /// <summary>
        /// Gets the current status of the DataImportHandler.
        /// </summary>
        /// <returns>DIH status</returns>
        SolrDIHStatus GetDIHStatus(KeyValuePair<string, string> options);
    }
}