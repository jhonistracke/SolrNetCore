
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.ResponseParsers {
    /// <summary>
    /// Parses spell-checking results from a query response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public class TermsResponseParser<T> : ISolrResponseParser<T> {
        public void Parse(XDocument xml, AbstractSolrQueryResults<T> results) {
            results.Switch(query: r => Parse(xml, r),
                           moreLikeThis: F.DoNothing);
        }

        public void Parse(XDocument xml, SolrQueryResults<T> results) {
            var termsNode = xml.XPathSelectElement("response/lst[@name='terms']");
            if (termsNode != null)
                results.Terms = ParseTerms(termsNode);
        }

        /// <summary>
        /// Parses spell-checking results
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public TermsResults ParseTerms(XElement node)
        {
            var r = new TermsResults();
            var terms = node.Elements("lst");
            foreach (var c in terms) {
                var result = new TermsResult();
                result.Field = c.Attribute("name").Value;
                var termList = new List<KeyValuePair<string, int>>();
                var termNodes = c.XPathSelectElements("int");
                foreach (var termNode in termNodes) {
                    termList.Add(new KeyValuePair<string, int>(termNode.Attribute("name").Value, int.Parse(termNode.Value)));
                }
                result.Terms = termList;
                r.Add(result);
            }
            return r;
        }
    }
}