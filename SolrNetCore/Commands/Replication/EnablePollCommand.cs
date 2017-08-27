namespace SolrNetCore.Commands.Replication
{
    /// <summary>
    /// Enable polling for changes from slave command
    /// http://wiki.apache.org/solr/SolrReplication
    /// https://cwiki.apache.org/confluence/display/solr/Index+Replication
    /// </summary>
	public class EnablePollCommand : ReplicationCommand
    {
        /// <summary>
        /// Enables the specified slave to poll for changes on the master. 
        /// </summary>
        public EnablePollCommand()
        {
            AddParameter("command", "enablepoll");
        }
    }
}