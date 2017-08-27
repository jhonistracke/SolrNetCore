using System;
using System.Collections.Generic;

namespace SolrNetCore.Impl.FacetQuerySerializers
{
    public class DefaultFacetQuerySerializer : ISolrFacetQuerySerializer
    {
        private readonly AggregateFacetQuerySerializer serializer;

        public DefaultFacetQuerySerializer(ISolrQuerySerializer querySerializer, ISolrFieldSerializer fieldSerializer)
        {
            serializer = new AggregateFacetQuerySerializer(new ISolrFacetQuerySerializer[] {
                new SolrFacetQuerySerializer(querySerializer),
                new SolrFacetDateQuerySerializer(fieldSerializer),
                new SolrFacetFieldQuerySerializer(),
                new SolrFacetPivotQuerySerializer()
            });
        }

        public bool CanHandleType(Type t)
        {
            return serializer.CanHandleType(t);
        }

        public IEnumerable<KeyValuePair<string, string>> Serialize(object q)
        {
            return serializer.Serialize(q);
        }
    }
}