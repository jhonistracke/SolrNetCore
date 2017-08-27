
using System;
using System.Collections.Generic;

namespace SolrNetCore.Impl.FieldSerializers {
    /// <summary>
    /// Aggregates the default <see cref="ISolrFieldSerializer"/>s
    /// </summary>
    public class DefaultFieldSerializer : ISolrFieldSerializer {
        private readonly AggregateFieldSerializer serializer;

        public DefaultFieldSerializer() {
            serializer = new AggregateFieldSerializer(new ISolrFieldSerializer[] {
                new CollectionFieldSerializer(this),
                new GenericDictionaryFieldSerializer(this),
                new NullableFieldSerializer(new BoolFieldSerializer()),
                new NullableFieldSerializer(new DateTimeFieldSerializer()),
                new NullableFieldSerializer(new DateTimeOffsetFieldSerializer()),
                new MoneyFieldSerializer(),
                new FormattableFieldSerializer(),
                new TypeConvertingFieldSerializer(),
            });
        }

        public bool CanHandleType(Type t) {
            return serializer.CanHandleType(t);
        }

        public IEnumerable<PropertyNode> Serialize(object obj) {
            return serializer.Serialize(obj);
        }
    }
}