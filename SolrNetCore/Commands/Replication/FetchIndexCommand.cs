using System.Collections.Generic;

namespace SolrNetCore.Commands.Replication
{
    /// <summary>
    /// Force a fetchindex on slave from master command
    /// http://wiki.apache.org/solr/SolrReplication
    /// https://cwiki.apache.org/confluence/display/solr/Index+Replication
    /// </summary>
	public class FetchIndexCommand : ReplicationCommand
    {
        /// <summary>
        /// Forces the specified slave to fetch a copy of the index from its master. If you like, you 
        /// can pass an extra attribute such as masterUrl or compression (or any other parameter which 
        /// is specified in the <lst name="slave" /> tag) to do a one time replication from a master. 
        /// This obviates the need for hard-coding the master in the slave. 
        /// </summary>
        /// <param name="parameters">Optional parameters</param>
        public FetchIndexCommand(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            AddParameter("command", "fetchindex");

            if (parameters != null)
                foreach (var kv in parameters)
                    AddParameter(kv.Key, kv.Value);
        }
    }
}