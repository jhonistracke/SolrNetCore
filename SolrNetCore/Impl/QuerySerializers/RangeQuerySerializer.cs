
using System;
using System.Globalization;
using System.Linq;

namespace SolrNetCore.Impl.QuerySerializers {
    public class RangeQuerySerializer : ISolrQuerySerializer {
        private readonly ISolrFieldSerializer fieldSerializer;

        public RangeQuerySerializer(ISolrFieldSerializer fieldSerializer) {
            this.fieldSerializer = fieldSerializer;
        }

        public bool CanHandleType(Type t) {
            return typeof (ISolrQueryByRange).IsAssignableFrom(t);
        }

        public static string BuildRange(string fieldName, string @from, string @to, bool inclusive) {
            return BuildRange(fieldName, @from, @to, inclusive, inclusive);
        }

        public static string BuildRange(string fieldName, string @from, string @to, bool inclusiveFrom, bool inclusiveTo) {
            return "$field:$ii$from TO $to$if"
                            .Replace("$field", QueryByFieldSerializer.EscapeSpaces(fieldName))
                            .Replace("$ii", inclusiveFrom ? "[" : "{")
                            .Replace("$if", inclusiveTo ? "]" : "}")
                            .Replace("$from", @from)
                            .Replace("$to", to);
        }

        public string SerializeValue(object o) {
            if (o == null)
                return "*";
            return fieldSerializer.Serialize(o).First().FieldValue;
        }

        public string Serialize(object q) {
            var query = (ISolrQueryByRange) q;
            return BuildRange(query.FieldName, 
                SerializeValue(query.From),
                SerializeValue(query.To),
                query.InclusiveFrom,
                query.InclusiveTo
                );
        }
    }
}