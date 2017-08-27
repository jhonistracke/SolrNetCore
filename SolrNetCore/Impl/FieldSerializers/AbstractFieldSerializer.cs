
using System;
using System.Collections.Generic;

namespace SolrNetCore.Impl.FieldSerializers {
    /// <summary>
    /// Strongly-typed abstract field parser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractFieldSerializer<T> : ISolrFieldSerializer {
        public abstract IEnumerable<PropertyNode> Parse(T obj);

        public bool CanHandleType(Type t) {
            return t == typeof (T);
        }

        public IEnumerable<PropertyNode> Serialize(object obj) {
            return Parse((T) obj);
        }
    }
}