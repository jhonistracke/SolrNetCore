
using System;

namespace SolrNetCore.Impl.QuerySerializers {
    public class HasValueQuerySerializer : SingleTypeQuerySerializer<SolrHasValueQuery> {
        private readonly ISolrQuerySerializer serializer;

        public HasValueQuerySerializer(ISolrQuerySerializer serializer) {
            this.serializer = serializer;
        }

        public override string Serialize(SolrHasValueQuery q) {
            return serializer.Serialize(new SolrQueryByRange<string>(q.Field, "*", "*"));
        }
    }
}