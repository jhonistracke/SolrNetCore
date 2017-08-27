
using System;
using System.Collections;
using System.Collections.Generic;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.FieldSerializers {
    /// <summary>
    /// Serializes <see cref="IDictionary{TKey,TValue}"/> properties
    /// </summary>
    public class GenericDictionaryFieldSerializer : ISolrFieldSerializer {
        private readonly ISolrFieldSerializer serializer;

        public GenericDictionaryFieldSerializer(ISolrFieldSerializer serializer) {
            this.serializer = serializer;
        }

        public bool CanHandleType(Type t) {
            return TypeHelper.IsGenericAssignableFrom(typeof (IDictionary<,>), t);
        }

        /// <summary>
        /// Gets the key from a <see cref="KeyValuePair{TKey,TValue}"/>
        /// </summary>
        /// <param name="kv"></param>
        /// <returns></returns>
        public string KVKey(object kv) {
            return kv.GetType().GetProperty("Key").GetValue(kv, null).ToString();
        }

        /// <summary>
        /// Gets the value from a <see cref="KeyValuePair{TKey,TValue}"/>
        /// </summary>
        /// <param name="kv"></param>
        /// <returns></returns>
        public object KVValue(object kv) {
            return kv.GetType().GetProperty("Value").GetValue(kv, null);
        }

        public IEnumerable<PropertyNode> Serialize(object obj) {
            if (obj == null)
                yield break;
            foreach (var de in (IEnumerable)obj) {
                var name = KVKey(de); 
                var value = serializer.Serialize(KVValue(de));
                if (value == null)
                    yield return new PropertyNode {FieldNameSuffix = name};
                else
                    foreach (var v in value)
                        yield return new PropertyNode {
                            FieldValue = v.FieldValue,
                            FieldNameSuffix = name,
                        };
            }
        }
    }
}