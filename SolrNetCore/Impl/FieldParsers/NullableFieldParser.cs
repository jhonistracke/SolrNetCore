
using System;
using System.Xml.Linq;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Wraps a <see cref="ISolrFieldParser"/> making it support the corresponding <see cref="Nullable{T}"/> type
    /// </summary>
    public class NullableFieldParser: ISolrFieldParser {
        private readonly ISolrFieldParser parser;

        public NullableFieldParser(ISolrFieldParser parser) {
            this.parser = parser;
        }

        public bool CanHandleSolrType(string solrType) {
            return parser.CanHandleSolrType(solrType);
        }

        public bool CanHandleType(Type t) {
            return parser.CanHandleType(t) || parser.CanHandleType(TypeHelper.GetUnderlyingNullableType(t));
        }

        public object Parse(XElement field, Type t) {
            if (string.IsNullOrEmpty(field.Value) && TypeHelper.IsNullableType(t))
                return null;
            return parser.Parse(field, t);
        }
    }
}