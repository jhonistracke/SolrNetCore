
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.FacetQuerySerializers {
    public class SolrFacetFieldQuerySerializer : SingleTypeFacetQuerySerializer<SolrFacetFieldQuery> {
        private static readonly Regex localParamsRx = new Regex(@"\{![^\}]+\}", RegexOptions.Compiled);

        public override IEnumerable<KeyValuePair<string, string>> Serialize(SolrFacetFieldQuery q) {
            yield return KV.Create("facet.field", q.Field);
            var fieldWithoutLocalParams = localParamsRx.Replace(q.Field, "");
            if (q.Prefix != null)
                yield return KV.Create(string.Format("f.{0}.facet.prefix", fieldWithoutLocalParams), q.Prefix);
            if (q.Sort.HasValue)
                yield return KV.Create(string.Format("f.{0}.facet.sort", fieldWithoutLocalParams), q.Sort.ToString().ToLowerInvariant());
            if (q.Limit.HasValue)
                yield return KV.Create(string.Format("f.{0}.facet.limit", fieldWithoutLocalParams), q.Limit.ToString());
            if (q.Offset.HasValue)
                yield return KV.Create(string.Format("f.{0}.facet.offset", fieldWithoutLocalParams), q.Offset.ToString());
            if (q.MinCount.HasValue)
                yield return KV.Create(string.Format("f.{0}.facet.mincount", fieldWithoutLocalParams), q.MinCount.ToString());
            if (q.Missing.HasValue)
                yield return KV.Create(string.Format("f.{0}.facet.missing", fieldWithoutLocalParams), q.Missing.ToString().ToLowerInvariant());
            if (q.EnumCacheMinDf.HasValue)
                yield return KV.Create(string.Format("f.{0}.facet.enum.cache.minDf", fieldWithoutLocalParams), q.EnumCacheMinDf.ToString());
        }
    }
}