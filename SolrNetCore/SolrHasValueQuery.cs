namespace SolrNetCore
{
    /// <summary>
    /// Queries documents that have any value in the specified field
    /// </summary>
    public class SolrHasValueQuery : AbstractSolrQuery
    {
        private readonly string field;

        public SolrHasValueQuery(string field)
        {
            this.field = field;
        }

        public string Field
        {
            get { return field; }
        }
    }
}