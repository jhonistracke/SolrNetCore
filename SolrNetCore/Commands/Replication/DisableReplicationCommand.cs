namespace SolrNetCore.Commands.Replication
{
    /// <summary>
    /// Disable replication on master for all slaves
    /// http://wiki.apache.org/solr/SolrReplication
    /// https://cwiki.apache.org/confluence/display/solr/Index+Replication
    /// </summary>
	public class DisableReplicationCommand : ReplicationCommand
    {
        /// <summary>
        /// Disables replication on the master for all its slaves. 
        /// </summary>
        public DisableReplicationCommand()
        {
            AddParameter("command", "disablereplication");
        }
    }
}