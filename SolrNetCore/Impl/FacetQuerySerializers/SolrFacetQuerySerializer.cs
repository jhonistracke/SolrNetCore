
using System;
using System.Collections.Generic;

namespace SolrNetCore.Impl.FacetQuerySerializers {
    public class SolrFacetQuerySerializer : SingleTypeFacetQuerySerializer<SolrFacetQuery> {
        private readonly ISolrQuerySerializer serializer;

        public SolrFacetQuerySerializer(ISolrQuerySerializer serializer) {
            this.serializer = serializer;
        }

        public override IEnumerable<KeyValuePair<string, string>> Serialize(SolrFacetQuery q) {
            yield return new KeyValuePair<string, string>("facet.query", serializer.Serialize(q.Query));
        }
    }
}