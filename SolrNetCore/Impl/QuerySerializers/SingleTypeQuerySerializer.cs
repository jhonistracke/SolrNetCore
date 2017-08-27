
using System;

namespace SolrNetCore.Impl.QuerySerializers {
    public abstract class SingleTypeQuerySerializer<T> : ISolrQuerySerializer {
        public bool CanHandleType(Type t) {
            return t == typeof (T);
        }

        public string Serialize(object q) {
            return Serialize((T)q);
        }

        public abstract string Serialize(T q);
    }
}