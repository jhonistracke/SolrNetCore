
using System;
using System.Collections.Generic;

namespace SolrNetCore.Impl.FacetQuerySerializers {
    public abstract class SingleTypeFacetQuerySerializer<T> : ISolrFacetQuerySerializer {
        public bool CanHandleType(Type t) {
            return t == typeof (T);
        }

        public IEnumerable<KeyValuePair<string, string>> Serialize(object q) {
            return Serialize((T) q);
        }

        public abstract IEnumerable<KeyValuePair<string, string>> Serialize(T q);
    }
}