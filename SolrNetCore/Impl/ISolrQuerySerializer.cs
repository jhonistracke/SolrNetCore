
using System;

namespace SolrNetCore.Impl {
    public interface ISolrQuerySerializer {
        bool CanHandleType(Type t);
        string Serialize(object q);
    }
}