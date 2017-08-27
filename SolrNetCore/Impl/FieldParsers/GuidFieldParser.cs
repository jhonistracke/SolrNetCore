
using System;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    public class GuidFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrType == "str";
        }

        public bool CanHandleType(Type t) {
            return t == typeof(Guid);
        }

        public object Parse(XElement field, Type t) {
            return new Guid(field.Value);
        }
    }
}