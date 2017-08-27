namespace SolrNetCore.Commands.Replication
{
    /// <summary>
    /// Get version number of the index.
    /// http://wiki.apache.org/solr/SolrReplication
    /// https://cwiki.apache.org/confluence/display/solr/Index+Replication
    /// </summary>
	public class IndexVersionCommand : ReplicationCommand
    {
        /// <summary>
        /// Returns the version of the latest replicatable index on the specified master or slave. 
        /// </summary>
        public IndexVersionCommand()
        {
            AddParameter("command", "indexversion");
        }
    }
}