
using System;
using System.Collections.Generic;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.FieldSerializers {
    /// <summary>
    /// Wraps a <see cref="ISolrFieldSerializer"/> making it support the corresponding <see cref="Nullable{T}"/> type
    /// </summary>
    public class NullableFieldSerializer : ISolrFieldSerializer {
        private readonly ISolrFieldSerializer serializer;

        public NullableFieldSerializer(ISolrFieldSerializer serializer) {
            this.serializer = serializer;
        }

        public bool CanHandleType(Type t) {
            return serializer.CanHandleType(t) || serializer.CanHandleType(TypeHelper.GetUnderlyingNullableType(t));
        }

        public IEnumerable<PropertyNode> Serialize(object obj) {
            if (obj == null)
                yield break;
            foreach (var i in serializer.Serialize(obj))
                yield return i;
        }
    }
}