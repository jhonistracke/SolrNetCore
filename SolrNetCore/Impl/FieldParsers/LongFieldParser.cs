
using System;
using System.Globalization;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses <see cref="long"/> fields
    /// </summary>
    public class LongFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrType == "long";
        }

        public bool CanHandleType(Type t) {
            return t == typeof (long);
        }

        public object Parse(XElement field, Type t) {
            return long.Parse(field.Value, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}