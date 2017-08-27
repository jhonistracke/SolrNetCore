namespace SolrNetCore.Commands.Replication
{
    /// <summary>
    /// Enable replication on master for all slaves
    /// http://wiki.apache.org/solr/SolrReplication
    /// https://cwiki.apache.org/confluence/display/solr/Index+Replication
    /// </summary>
	public class EnableReplicationCommand : ReplicationCommand
    {
        /// <summary>
        /// Enables replication on the master for all its slaves. 
        /// </summary>
        public EnableReplicationCommand()
        {
            AddParameter("command", "enablereplication");
        }
    }
}