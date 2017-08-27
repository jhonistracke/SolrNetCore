
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.FieldParsers {
    /// <summary>
    /// Aggregates <see cref="ISolrFieldParser"/>s
    /// </summary>
    public class AggregateFieldParser : ISolrFieldParser {
        private readonly IEnumerable<ISolrFieldParser> parsers;

        /// <summary>
        /// Aggregates <see cref="ISolrFieldParser"/>s
        /// </summary>
        /// <param name="parsers"></param>
        public AggregateFieldParser(IEnumerable<ISolrFieldParser> parsers) {
            this.parsers = parsers;
        }

        public bool CanHandleSolrType(string solrType) {
            return parsers.Any(p => p.CanHandleSolrType(solrType));
        }

        public bool CanHandleType(Type t) {
            return parsers.Any(p => p.CanHandleType(t));
        }

        public object Parse(XElement field, Type t) {
            return parsers
                .Where(p => p.CanHandleType(t) && p.CanHandleSolrType(field.Name.LocalName))
                .Select(p => p.Parse(field, t))
                .FirstOrDefault();
        }
    }
}