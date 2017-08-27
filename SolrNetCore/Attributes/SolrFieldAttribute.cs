using System;

namespace SolrNetCore.Attributes
{
    /// <summary>
    /// Marks a property as present on Solr. By default the Solr field name is the property name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SolrFieldAttribute : Attribute
    {
        /// <summary>
        /// Marks a property as present on Solr. By default the Solr field name is the property name
        /// </summary>
		public SolrFieldAttribute() { }

        /// <summary>
        /// Marks a property as present on Solr with the defined Solr field name
        /// </summary>
        /// <param name="fieldName"></param>
        public SolrFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        /// <summary>
        /// Overrides Solr field name
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Adds an index time boost to a field.
        /// </summary>
        public float Boost { get; set; }
    }
}