
using System;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    public class EnumFieldParser : ISolrFieldParser {
        public bool CanHandleSolrType(string solrType) {
            return solrType == "str" || solrType == "int";
        }

        public bool CanHandleType(Type t) {
            return t.IsEnum;
        }

        public object Parse(XElement field, Type t) {
            if (field == null)
                throw new ArgumentNullException("field");
            if (t == null)
                throw new ArgumentNullException("t");
            var value = field.Value;
            try {
                return Enum.Parse(t, field.Value);
            } catch (Exception e) {
                throw new Exception(string.Format("Invalid value '{0}' for enum type '{1}'", value, t), e);
            }
        }
    }
}