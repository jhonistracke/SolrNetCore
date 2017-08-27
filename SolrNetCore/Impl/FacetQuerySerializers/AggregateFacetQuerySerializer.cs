using SolrNetCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrNetCore.Impl.FacetQuerySerializers
{
    public class AggregateFacetQuerySerializer : ISolrFacetQuerySerializer
    {
        private readonly ISolrFacetQuerySerializer[] serializers;

        public AggregateFacetQuerySerializer(ISolrFacetQuerySerializer[] serializers)
        {
            this.serializers = serializers;
        }

        public bool CanHandleType(Type t)
        {
            return serializers.Any(s => s.CanHandleType(t));
        }

        public IEnumerable<KeyValuePair<string, string>> Serialize(object q)
        {
            if (q == null)
                yield break;
            var t = q.GetType();
            foreach (var s in serializers)
                if (s.CanHandleType(t))
                {
                    foreach (var k in s.Serialize(q))
                        yield return k;
                    yield break;
                }
            throw new SolrNetException(string.Format("Couldn't serialize facet query '{0}' of type '{1}'", q, t));
        }
    }
}