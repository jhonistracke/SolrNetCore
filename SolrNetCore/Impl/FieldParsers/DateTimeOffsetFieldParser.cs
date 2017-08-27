
using System;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses <see cref="DateTimeOffset"/> fields
    /// </summary>
    public class DateTimeOffsetFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrType == "date";
        }

        public bool CanHandleType(Type t) {
            return t == typeof (DateTimeOffset);
        }

        public object Parse(XElement field, Type t) {
            return Parse(field.Value);
        }

        public static DateTimeOffset Parse(string s) {
            var t = DateTimeFieldParser.ParseDate(s);
            return new DateTimeOffset(t, TimeSpan.Zero);
        }
    }
}