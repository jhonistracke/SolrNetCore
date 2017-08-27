
using System;
using System.Globalization;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses double fields
    /// </summary>
    public class DoubleFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrType == "float" || solrType == "int";
        }

        public bool CanHandleType(Type t) {
            return t == typeof (double);
        }

        public object Parse(XElement field, Type t) {
            return double.Parse(field.Value, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}