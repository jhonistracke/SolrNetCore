
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SolrNetCore.Mapping {
    /// <summary>
    /// Maps all properties in the class, with the same field name as the property.
    /// Note that unique keys must be added manually.
    /// </summary>
    public class AllPropertiesMappingManager : IReadOnlyMappingManager {
        private readonly IDictionary<Type, PropertyInfo> uniqueKeys = new Dictionary<Type, PropertyInfo>();

        public IDictionary<string,SolrFieldModel> GetFields(Type type) {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var fldProps = props
                .Select(prop => new SolrFieldModel(prop, prop.Name, null))
                .Select(m => new KeyValuePair<string, SolrFieldModel>(m.FieldName, m))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            return fldProps;
        }

        public SolrFieldModel GetUniqueKey(Type type) {
            try {
                var propertyInfo = uniqueKeys[type];
	            return new SolrFieldModel(propertyInfo, propertyInfo.Name, null);
            } catch (KeyNotFoundException) {
                return null;
            }
        }

        public ICollection<Type> GetRegisteredTypes() {
            return new List<Type>();
        }

        /// <summary>
        /// Sets the property that acts as unique key for a document type
        /// </summary>
        /// <param name="property">Unique key property</param>
        public void SetUniqueKey(PropertyInfo property) {
            if (property == null)
                throw new ArgumentNullException("property");
            var t = property.ReflectedType;
            uniqueKeys[t] = property;
        }
    }
}