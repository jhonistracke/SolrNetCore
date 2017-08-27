using System;
using System.Collections.Generic;

namespace SolrNetCore
{
    /// <summary>
    /// Service interface for mapping Solr fields to object properties
    /// </summary>
    public interface IReadOnlyMappingManager
    {
        /// <summary>
        /// Gets fields mapped for this type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Empty collection if <paramref name="type"/> is not mapped</returns>
        IDictionary<string, SolrFieldModel> GetFields(Type type);

        /// <summary>
        /// Gets unique key for the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Null if type has no unique key defined</returns>
        SolrFieldModel GetUniqueKey(Type type);

        /// <summary>
        /// Gets all registered document types in this mapping manager
        /// </summary>
        /// <returns></returns>
        ICollection<Type> GetRegisteredTypes();
    }
}