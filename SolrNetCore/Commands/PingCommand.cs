using System.Collections.Generic;

namespace SolrNetCore.Commands
{
    /// <summary>
    /// Pings the Solr server.
    /// It can be used by a load balancer in front of a set of Solr servers to check response time of all the Solr servers in order to do response time based load balancing.
    /// </summary>
    /// <seealso href="http://wiki.apache.org/solr/SolrConfigXml"/>
	public class PingCommand : ISolrCommand
    {
        public string Execute(ISolrConnection connection)
        {
            return connection.Get("/admin/ping", new Dictionary<string, string>());
        }
    }
}