using SolrNetCore.Exceptions;
using System;
using System.Linq;

namespace SolrNetCore.Impl.QuerySerializers
{
    public class AggregateQuerySerializer : ISolrQuerySerializer
    {
        private readonly ISolrQuerySerializer[] serializers;

        public AggregateQuerySerializer(ISolrQuerySerializer[] serializers)
        {
            this.serializers = serializers;
        }

        public bool CanHandleType(Type t)
        {
            return serializers.Any(s => s.CanHandleType(t));
        }

        public string Serialize(object q)
        {
            if (q == null)
                return string.Empty;
            var t = q.GetType();
            foreach (var s in serializers)
                if (s.CanHandleType(t))
                    return s.Serialize(q);
            throw new SolrNetException(string.Format("Couldn't serialize query '{0}' of type '{1}'", q, t));
        }
    }
}