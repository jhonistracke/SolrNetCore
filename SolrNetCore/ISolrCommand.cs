namespace SolrNetCore
{
    /// <summary>
    /// Command interface
    /// </summary>
	public interface ISolrCommand
    {
        /// <summary>
        /// Executes this command
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
		string Execute(ISolrConnection connection);
    }
}