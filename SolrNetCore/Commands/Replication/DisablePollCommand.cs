namespace SolrNetCore.Commands.Replication
{
    /// <summary>
    /// Disable polling for changes from slave command
    /// http://wiki.apache.org/solr/SolrReplication
    /// https://cwiki.apache.org/confluence/display/solr/Index+Replication
    /// </summary>
    public class DisablePollCommand : ReplicationCommand
    {
        /// <summary>
        /// Disables the specified slave from polling for changes on the master. 
        /// </summary>
        public DisablePollCommand()
        {
            AddParameter("command", "disablepoll");
        }
    }
}