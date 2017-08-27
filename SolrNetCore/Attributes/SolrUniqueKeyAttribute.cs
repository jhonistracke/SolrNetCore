using System;

namespace SolrNetCore.Attributes
{
    /// <summary>
    /// Marks a property as unique key. By default the Solr field name is the property name.
    /// </summary>
	[AttributeUsage(AttributeTargets.Property)]
    public class SolrUniqueKeyAttribute : SolrFieldAttribute
    {
        /// <summary>
        /// Marks a property as unique key. By default the Solr field name is the property name.
        /// </summary>
		public SolrUniqueKeyAttribute() { }

        /// <summary>
        /// Marks a property as unique key.
        /// </summary>
        /// <param name="fieldName"></param>
		public SolrUniqueKeyAttribute(string fieldName) : base(fieldName) { }
    }
}