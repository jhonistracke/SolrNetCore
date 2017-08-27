
using System;
using System.Globalization;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses int values
    /// </summary>
    public class IntFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrType == "int";
        }

        public bool CanHandleType(Type t) {
            return typeof (int) == t;
        }

        public object Parse(XElement field, Type t) {
            return int.Parse(field.Value, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}