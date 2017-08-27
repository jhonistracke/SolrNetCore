
using System;

namespace SolrNetCore.Impl.QuerySerializers {
    public class SelfSerializingQuerySerializer : ISolrQuerySerializer {
        public bool CanHandleType(Type t) {
            return typeof (ISelfSerializingQuery).IsAssignableFrom(t);
        }

        public string Serialize(object q) {
            var sq = (ISelfSerializingQuery)q;
            return sq.Query;
        }
    }
}