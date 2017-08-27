
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.ResponseParsers {
    /// <summary>
    /// Parses highlighting results from a query response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public class HighlightingResponseParser<T> : ISolrResponseParser<T> {
        public void Parse(XDocument xml, AbstractSolrQueryResults<T> results) {
            results.Switch(query: r => Parse(xml, r),
                           moreLikeThis: F.DoNothing);
        }

        public void Parse(XDocument xml, SolrQueryResults<T> results) {
            var highlightingNode = xml.XPathSelectElement("response/lst[@name='highlighting']");
            if (highlightingNode != null)
                results.Highlights = ParseHighlighting(results, highlightingNode);
        }

        /// <summary>
        /// Parses highlighting results
        /// </summary>
        /// <param name="results"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IDictionary<string, HighlightedSnippets> ParseHighlighting(IEnumerable<T> results, XElement node) {
            var highlights = new Dictionary<string, HighlightedSnippets>();
            var docRefs = node.Elements("lst");
            foreach (var docRef in docRefs) {
                var docRefKey = docRef.Attribute("name").Value;
                highlights.Add(docRefKey, ParseHighlightingFields(docRef.Elements()));                    
            }
            return highlights;
        }

        /// <summary>
        /// Parse highlighting snippets for each field.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static HighlightedSnippets ParseHighlightingFields(IEnumerable<XElement> nodes) {
            var fields = new HighlightedSnippets();
            foreach (var field in nodes) {
                var fieldName = field.Attribute("name").Value;
                ICollection<string> snippets = field.Elements("str")
                    .Select(str => str.Value)
                    .ToList();
                if (snippets.Count == 0 && !string.IsNullOrEmpty(field.Value))
                    snippets = new[] { field.Value };
                fields.Add(fieldName, snippets);
            }
            return fields;
        }
    }
}