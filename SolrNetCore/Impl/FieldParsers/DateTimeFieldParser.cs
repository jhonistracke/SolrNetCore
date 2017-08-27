
using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses <see cref="DateTime"/> fields
    /// </summary>
    public class DateTimeFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrType == "date";
        }

        public bool CanHandleType(Type t) {
            return t == typeof (DateTime);
        }

        public object Parse(XElement field, Type t) {
            return ParseDate(field.Value);
        }

        public static DateTime ParseDate(string s) {
            var p = s.Split('-');
            s = p[0].PadLeft(4, '0') + '-' + string.Join("-", p.Skip(1).ToArray());          
            
            // Mono does not support that exact format string for some reason, however Parse appears to properly handle the input. 
            // Try using the format string, and if that fails, fall back to just a naive Parse.
            DateTime result;
            if (!DateTime.TryParseExact(s, "yyyy-MM-dd'T'HH:mm:ss.FFF'Z'", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                result = DateTime.Parse(s, CultureInfo.InvariantCulture);
            }

            if (result.Kind == DateTimeKind.Unspecified)
            {
                result = DateTime.SpecifyKind(result, DateTimeKind.Utc);
            }

            return result;
        }
    }
}