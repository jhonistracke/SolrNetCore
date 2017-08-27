
using System.Reflection;

namespace SolrNetCore.Mapping {
    /// <summary>
    /// Mapping manager abstraction
    /// </summary>
    public interface IMappingManager : IReadOnlyMappingManager {
        /// <summary>
        /// Maps a property with the property name as Solr field name
        /// </summary>
        /// <param name="property">Document type property</param>
        void Add(PropertyInfo property);

        /// <summary>
        /// Maps a property
        /// </summary>
        /// <param name="property">Document type property</param>
        /// <param name="fieldName">Solr field name</param>
        void Add(PropertyInfo property, string fieldName);

        /// <summary>
        /// Maps a property
        /// </summary>
        /// <param name="property">Document type property</param>
        /// <param name="fieldName">Solr field name</param>
        /// <param name="boost">Index-time boosting</param>
        void Add(PropertyInfo property, string fieldName, float? boost);

        /// <summary>
        /// Sets unique key for a document type
        /// </summary>
        /// <param name="property">Document type property</param>
        void SetUniqueKey(PropertyInfo property);
    }
}