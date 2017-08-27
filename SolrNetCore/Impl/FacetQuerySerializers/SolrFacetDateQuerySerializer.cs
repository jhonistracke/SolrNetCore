
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.FacetQuerySerializers {
    public class SolrFacetDateQuerySerializer : SingleTypeFacetQuerySerializer<SolrFacetDateQuery> {

        private static readonly Regex localParamsRx = new Regex(@"\{![^\}]+\}", RegexOptions.Compiled);

        private readonly ISolrFieldSerializer fieldSerializer;

        public SolrFacetDateQuerySerializer(ISolrFieldSerializer fieldSerializer) {
            this.fieldSerializer = fieldSerializer;
        }

        public string SerializeSingle(object o) {
            return fieldSerializer.Serialize(o).First().FieldValue;
        }

        public override IEnumerable<KeyValuePair<string, string>> Serialize(SolrFacetDateQuery q) {
            var fieldWithoutLocalParams = localParamsRx.Replace(q.Field, ""); 
            yield return KV.Create("facet.date", q.Field);
            yield return KV.Create(string.Format("f.{0}.facet.date.start", fieldWithoutLocalParams), SerializeSingle(q.Start));
            yield return KV.Create(string.Format("f.{0}.facet.date.end", fieldWithoutLocalParams), SerializeSingle(q.End));
            yield return KV.Create(string.Format("f.{0}.facet.date.gap", fieldWithoutLocalParams), q.Gap);
            if (q.HardEnd.HasValue)
                yield return KV.Create(string.Format("f.{0}.facet.date.hardend", fieldWithoutLocalParams), SerializeSingle(q.HardEnd.Value));
            if (q.Other != null && q.Other.Count > 0)
                foreach (var o in q.Other)
                    yield return KV.Create(string.Format("f.{0}.facet.date.other", fieldWithoutLocalParams), o.ToString());
            if (q.Include != null && q.Include.Count > 0)
                foreach (var i in q.Include)
                    yield return KV.Create(string.Format("f.{0}.facet.date.include", fieldWithoutLocalParams), i.ToString());
        }
    }
}
