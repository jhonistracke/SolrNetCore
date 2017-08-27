
using System;
using System.Globalization;

namespace SolrNetCore.Impl.QuerySerializers {
    public class BoostQuerySerializer : SingleTypeQuerySerializer<SolrQueryBoost> {
        private readonly ISolrQuerySerializer serializer;

        public BoostQuerySerializer(ISolrQuerySerializer serializer) {
            this.serializer = serializer;
        }

        public override string Serialize(SolrQueryBoost q) {
            return string.Format("({0})^{1}", serializer.Serialize(q.Query), q.Factor.ToString(CultureInfo.InvariantCulture.NumberFormat));
        }
    }
}