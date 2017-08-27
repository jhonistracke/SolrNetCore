
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.ResponseParsers {
    /// <summary>
    /// Parses more-like-this results from a query response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MoreLikeThisResponseParser<T> : ISolrResponseParser<T> {
        private readonly ISolrDocumentResponseParser<T> docParser;

        public void Parse(XDocument xml, AbstractSolrQueryResults<T> results) {
            results.Switch(query: r => Parse(xml, r), 
                           moreLikeThis: F.DoNothing);
        }

        public MoreLikeThisResponseParser(ISolrDocumentResponseParser<T> docParser) {
            this.docParser = docParser;
        }

        public void Parse(XDocument xml, SolrQueryResults<T> results) {
            var moreLikeThis = xml.XPathSelectElement("response/lst[@name='moreLikeThis']");
            if (moreLikeThis != null)
                results.SimilarResults = ParseMoreLikeThis(results, moreLikeThis);
        }

        /// <summary>
        /// Parses more-like-this results
        /// </summary>
        /// <param name="results"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public IDictionary<string, IList<T>> ParseMoreLikeThis(IEnumerable<T> results, XElement node) {
            var r = new Dictionary<string, IList<T>>();
            var docRefs = node.Elements("result");
            foreach (var docRef in docRefs) {
                var docRefKey = docRef.Attribute("name").Value;
                r[docRefKey] = docParser.ParseResults(docRef);
            }
            return r;
        }
    }
}