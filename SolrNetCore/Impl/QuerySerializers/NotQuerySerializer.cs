
using System;

namespace SolrNetCore.Impl.QuerySerializers {
    public class NotQuerySerializer : SingleTypeQuerySerializer<SolrNotQuery> {
        private readonly ISolrQuerySerializer serializer;

        public NotQuerySerializer(ISolrQuerySerializer serializer) {
            this.serializer = serializer;
        }

        public override string Serialize(SolrNotQuery q) {
            return "-" + serializer.Serialize(q.Query);
        }
    }
}