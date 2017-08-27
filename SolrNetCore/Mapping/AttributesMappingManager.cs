using SolrNetCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SolrNetCore.Mapping
{
    /// <summary>
    /// Gets mapping info from attributes like <see cref="SolrFieldAttribute"/> and <see cref="SolrUniqueKeyAttribute"/>
    /// </summary>
    public class AttributesMappingManager : IReadOnlyMappingManager
    {
        public virtual IEnumerable<KeyValuePair<PropertyInfo, T[]>> GetPropertiesWithAttribute<T>(Type type) where T : Attribute
        {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var kvAttrs = props.Select(prop => new KeyValuePair<PropertyInfo, T[]>(prop, GetCustomAttributes<T>(prop)));
            var propsAttrs = kvAttrs.Where(kv => kv.Value.Length > 0);
            return propsAttrs;
        }

        public IDictionary<string, SolrFieldModel> GetFields(Type type)
        {
            var propsAttrs = GetPropertiesWithAttribute<SolrFieldAttribute>(type);

            var fields = propsAttrs
                .Select(kv => new SolrFieldModel(
                                  property: kv.Key,
                                  fieldName: kv.Value[0].FieldName ?? kv.Key.Name,
                                  boost: kv.Value[0].Boost))
                .Select(m => new KeyValuePair<string, SolrFieldModel>(m.FieldName, m))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            return fields;
        }

        public virtual T[] GetCustomAttributes<T>(PropertyInfo prop) where T : Attribute
        {
            return (T[])prop.GetCustomAttributes(typeof(T), true);
        }

        public SolrFieldModel GetUniqueKey(Type type)
        {
            var propsAttrs = GetPropertiesWithAttribute<SolrUniqueKeyAttribute>(type);
            var fields = propsAttrs.Select(
                kv => new SolrFieldModel(
                          property: kv.Key,
                          fieldName: kv.Value[0].FieldName ?? kv.Key.Name,
                          boost: null
                          ));
            return fields.FirstOrDefault();
        }

        public ICollection<Type> GetRegisteredTypes()
        {
            var types = new List<Type>();
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (var t in a.GetTypes())
                    {
                        if (GetFields(t).Count > 0)
                            types.Add(t);
                    }
                }
                catch (ReflectionTypeLoadException)
                {
                    // if I can't get an assembly's types, just ignore it
                }
            }
            return types;
        }
    }
}