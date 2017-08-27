namespace SolrNetCore.Commands.Replication
{
    /// <summary>
    /// Abort copying index from master to slave command.
    /// http://wiki.apache.org/solr/SolrReplication
    /// https://cwiki.apache.org/confluence/display/solr/Index+Replication
    /// </summary>
    public class AbortFetchCommand : ReplicationCommand
    {
        /// <summary>
        /// Aborts copying an index from a master to the specified slave.
        /// </summary>
        public AbortFetchCommand()
        {
            AddParameter("command", "abortfetch");
        }
    }
}