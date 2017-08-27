
using System;
using System.Collections.Generic;

namespace SolrNetCore.Impl {
    public interface ISolrFacetQuerySerializer {
        bool CanHandleType(Type t);
        IEnumerable<KeyValuePair<string, string>> Serialize(object q);
    }
}