
using System;
using System.Globalization;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses float values
    /// </summary>
    public class FloatFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrType == "float" || solrType == "int";
        }

        public bool CanHandleType(Type t) {
            return t == typeof (float);
        }

        public static float Parse(string value) {
            return float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }

        public static float Parse(XElement field) {
            if (field == null)
                throw new ArgumentNullException("field");
            return Parse(field.Value);
        }

        public object Parse(XElement field, Type t) {
            return Parse(field);
        }
    }
}