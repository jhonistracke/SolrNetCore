
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Parser that infers .net type based on solr type
    /// </summary>
    public class InferringFieldParser : ISolrFieldParser {
        private readonly ISolrFieldParser parser;

        public InferringFieldParser(ISolrFieldParser parser) {
            this.parser = parser;
        }

        public bool CanHandleSolrType(string solrType) {
            return true;
        }

        public bool CanHandleType(Type t) {
            return true;
        }

        private static readonly IDictionary<string, Type> solrTypes;

        static InferringFieldParser() {
            solrTypes = new Dictionary<string, Type> {
                {"bool", typeof (bool)},
                {"str", typeof (string)},
                {"int", typeof (int)},
                {"float", typeof (float)},
                {"double", typeof(double)},
                {"long", typeof (long)},
                {"arr", typeof (ICollection)},
                {"date", typeof (DateTime)},
            };
        }

        public object Parse(XElement field, Type t) {
            var type = solrTypes[field.Name.LocalName];
            return parser.Parse(field, type);
        }
    }
}