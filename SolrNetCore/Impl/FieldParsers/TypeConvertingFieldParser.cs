
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parses using <see cref="TypeConverter"/>
    /// </summary>
    public class TypeConvertingFieldParser: ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrTypes.ContainsKey(solrType);
        }

        public bool CanHandleType(Type t) {
            return solrTypes.Values.Contains(t);
        }

        private static readonly IDictionary<string, Type> solrTypes;

        static TypeConvertingFieldParser() {
            solrTypes = new Dictionary<string, Type> {
                {"bool", typeof (bool)},
                {"str", typeof (string)},
                {"int", typeof (int)},
                {"float", typeof (float)},
                {"double", typeof(double)},
                {"long", typeof (long)},
            };
        }

        /// <summary>
        /// Gets the corresponding CLR Type to a solr type
        /// </summary>
        /// <param name="field"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public Type GetUnderlyingType(XElement field, Type t) {
            if (t != typeof(object))
                return t;
            return solrTypes[field.Name.LocalName];
        }

        public object Parse(XElement field, Type t) {
            var converter = TypeDescriptor.GetConverter(GetUnderlyingType(field, t));
            if (converter != null && converter.CanConvertFrom(typeof(string)))
                return converter.ConvertFromInvariantString(field.Value);
            return Convert.ChangeType(field.Value, t);
        }
    }
}