
using System;

namespace SolrNetCore.Impl.QuerySerializers {
    public class DefaultQuerySerializer : ISolrQuerySerializer {
        private readonly AggregateQuerySerializer serializer;

        public DefaultQuerySerializer(ISolrFieldSerializer fieldSerializer) {
            serializer = new AggregateQuerySerializer(new ISolrQuerySerializer[] {
                new QueryByFieldSerializer(),
                new QueryByFieldRegexSerializer(),
                new LocalParamsSerializer(this),
                new BoostQuerySerializer(this),
                new HasValueQuerySerializer(this),
                new NotQuerySerializer(this),
                new RequiredQuerySerializer(this),
                new QueryInListSerializer(this),
                new NullableDateTimeRangeQuerySerializer(fieldSerializer),
                new DateTimeRangeQuerySerializer(fieldSerializer),
                new RangeQuerySerializer(fieldSerializer),
                new MultipleCriteriaQuerySerializer(this),
                new SelfSerializingQuerySerializer(),
            });
        }

        public bool CanHandleType(Type t) {
            return serializer.CanHandleType(t);
        }

        public string Serialize(object q) {
            return serializer.Serialize(q);
        }
    }
}