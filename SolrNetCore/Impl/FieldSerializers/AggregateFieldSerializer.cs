using SolrNetCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrNetCore.Impl.FieldSerializers
{
    /// <summary>
    /// Aggregates <see cref="ISolrFieldSerializer"/>s
    /// </summary>
    public class AggregateFieldSerializer : ISolrFieldSerializer
    {
        private readonly IEnumerable<ISolrFieldSerializer> serializers;

        public AggregateFieldSerializer(IEnumerable<ISolrFieldSerializer> serializers)
        {
            this.serializers = serializers;
        }

        public bool CanHandleType(Type t)
        {
            return serializers.Any(s => s.CanHandleType(t));
        }

        public IEnumerable<PropertyNode> Serialize(object obj)
        {
            if (obj == null)
                return null;
            var type = obj.GetType();
            foreach (var s in serializers)
                if (s.CanHandleType(type))
                    return s.Serialize(obj);
            throw new TypeNotSupportedException(string.Format("Couldn't serialize type '{0}'", type));
        }
    }
}