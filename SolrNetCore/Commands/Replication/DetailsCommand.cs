namespace SolrNetCore.Commands.Replication
{
    /// <summary>
    /// Get all the details of the configuration and current status
    /// http://wiki.apache.org/solr/SolrReplication
    /// https://cwiki.apache.org/confluence/display/solr/Index+Replication
    /// </summary>
	public class DetailsCommand : ReplicationCommand
    {
        /// <summary>
        /// Retrieves configuration details and current status. 
        /// </summary>
        public DetailsCommand()
        {
            AddParameter("command", "details");
        }
    }
}