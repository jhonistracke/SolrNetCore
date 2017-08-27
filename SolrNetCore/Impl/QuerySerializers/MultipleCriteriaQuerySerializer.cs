
using System.Text;

namespace SolrNetCore.Impl.QuerySerializers {
    public class MultipleCriteriaQuerySerializer : SingleTypeQuerySerializer<SolrMultipleCriteriaQuery> {
        private readonly ISolrQuerySerializer serializer;

        public MultipleCriteriaQuerySerializer(ISolrQuerySerializer serializer) {
            this.serializer = serializer;
        }

        public override string Serialize(SolrMultipleCriteriaQuery q) {
            var queryBuilder = new StringBuilder();
            foreach (var query in q.Queries) {
                if (query == null)
                    continue;
                var sq = serializer.Serialize(query);
                if (string.IsNullOrEmpty(sq))
                    continue;
                if (queryBuilder.Length > 0)
                    queryBuilder.AppendFormat(" {0} ", q.Oper);
                queryBuilder.Append(sq);
            }
            var queryString = queryBuilder.ToString();
            if (!string.IsNullOrEmpty(queryString))
                queryString = "(" + queryString + ")";
            return queryString;
        }
    }
}
