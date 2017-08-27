
using System;

namespace SolrNetCore.Impl.QuerySerializers {
    public class RequiredQuerySerializer : SingleTypeQuerySerializer<SolrRequiredQuery> {
        private readonly ISolrQuerySerializer serializer;

        public RequiredQuerySerializer(ISolrQuerySerializer serializer) {
            this.serializer = serializer;
        }

        public override string Serialize(SolrRequiredQuery q) {
            return "+" + serializer.Serialize(q.Query);
        }
    }
}