
using System;
using System.Globalization;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses <see cref="decimal"/> fields
    /// </summary>
    public class DecimalFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return true;
        }

        public bool CanHandleType(Type t) {
            return t == typeof (decimal);
        }

        public object Parse(XElement field, Type t) {
            return decimal.Parse(field.Value, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}