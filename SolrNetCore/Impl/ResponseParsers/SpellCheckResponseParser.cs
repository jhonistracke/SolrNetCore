
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using SolrNetCore.Utils;

namespace SolrNetCore.Impl.ResponseParsers
{
    /// <summary>
    /// Parses spell-checking results from a query response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public class SpellCheckResponseParser<T> : ISolrResponseParser<T>
    {
        public void Parse(XDocument xml, AbstractSolrQueryResults<T> results)
        {
            results.Switch(query: r => Parse(xml, r),
                           moreLikeThis: F.DoNothing);
        }

        public void Parse(XDocument xml, SolrQueryResults<T> results)
        {
            var spellCheckingNode = xml.XPathSelectElement("response/lst[@name='spellcheck']");
            if (spellCheckingNode != null)
                results.SpellChecking = ParseSpellChecking(spellCheckingNode);
        }

        /// <summary>
        /// Parses spell-checking results
        /// </summary>
        /// <param name="node">SpellCheck node</param>
        /// <returns>List of suggestions and collations</returns>
        public SpellCheckResults ParseSpellChecking(XElement node)
        {
            var r = new SpellCheckResults();
            var suggestionsNode = node.XPathSelectElement("lst[@name='suggestions']");

            var collationNode = suggestionsNode.XPathSelectElement("str[@name='collation']");
            if (collationNode != null)
            {
                r.Collation = collationNode.Value;
            }

            IEnumerable<XElement> collationNodes;
            var collationsNode = node.XPathSelectElement("lst[@name='collations']");
            if (collationsNode != null)
            {
                // Solr 5.0+
                collationNodes = collationsNode.XPathSelectElements("lst[@name='collation']");
            }
            else
            {
                // Solr 4.x and lower
                collationNodes = suggestionsNode.XPathSelectElements("lst[@name='collation']");
            }

            foreach (var cn in collationNodes)
            {
                if (cn.XPathSelectElement("str[@name='collationQuery']") != null)
                {
                    r.Collations.Add(cn.XPathSelectElement("str[@name='collationQuery']").Value);
                    if (string.IsNullOrEmpty(r.Collation))
                    {
                        r.Collation = cn.XPathSelectElement("str[@name='collationQuery']").Value;
                    }
                }
            }

            var spellChecks = suggestionsNode.Elements("lst");
            foreach (var c in spellChecks)
            {
                if (c.Attribute("name").Value != "collation" || c.XPathSelectElement("int[@name='numFound']") != null)
                {
                    //Spelling suggestions are added, required to check if 'collation' is a search term or indicates collation node
                    var result = new SpellCheckResult();
                    result.Query = c.Attribute("name").Value;
                    result.NumFound = Convert.ToInt32(c.XPathSelectElement("int[@name='numFound']").Value);
                    result.EndOffset = Convert.ToInt32(c.XPathSelectElement("int[@name='endOffset']").Value);
                    result.StartOffset = Convert.ToInt32(c.XPathSelectElement("int[@name='startOffset']").Value);
                    var suggestions = new List<string>();
                    var suggestionNodes = c.XPathSelectElements("arr[@name='suggestion']/str");
                    foreach (var suggestionNode in suggestionNodes)
                    {
                        suggestions.Add(suggestionNode.Value);
                    }
                    result.Suggestions = suggestions;
                    r.Add(result);
                }
            }

            return r;
        }
    }
}