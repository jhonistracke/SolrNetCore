
using System;
using System.Collections.Generic;

namespace SolrNetCore.Impl.FieldSerializers {
    /// <summary>
    /// Serializes using .ToString()
    /// </summary>
    public class ToStringFieldSerializer: ISolrFieldSerializer {
        /// <summary>
        /// True if this serializer can handle the type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool CanHandleType(Type t) {
            return true;
        }

        public IEnumerable<PropertyNode> Serialize(object obj) {
            if (obj == null)
                yield break;
            yield return new PropertyNode {
                FieldValue = obj.ToString(),
            };
        }
    }
}