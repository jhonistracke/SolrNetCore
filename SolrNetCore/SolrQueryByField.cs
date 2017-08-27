namespace SolrNetCore
{
    /// <summary>
    /// Queries a field for a value
    /// </summary>
	public class SolrQueryByField : AbstractSolrQuery
    {
        private readonly string fieldName;
        private readonly string fieldValue;

        /// <summary>
        /// Queries a field for a value
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="fieldValue">Field value</param>
        public SolrQueryByField(string fieldName, string fieldValue)
        {
            this.fieldName = fieldName;
            this.fieldValue = fieldValue;
            Quoted = true;
        }

        /// <summary>
        /// If true (default), special characters (e.g. '?', '*') in the value are quoted.
        /// </summary>
        public bool Quoted { get; set; }

        public string FieldName
        {
            get { return fieldName; }
        }

        public string FieldValue
        {
            get { return fieldValue; }
        }
    }
}