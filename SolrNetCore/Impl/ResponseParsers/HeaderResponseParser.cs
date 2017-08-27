
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SolrNetCore.Impl.ResponseParsers {
    /// <summary>
    /// Parses header (status, QTime, etc) from a query response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public class HeaderResponseParser<T> : ISolrAbstractResponseParser<T>, ISolrHeaderResponseParser
    {
        public void Parse(XDocument xml, AbstractSolrQueryResults<T> results) {
            var header = Parse(xml);
            if (header != null)
                results.Header = header;
        }

        /// <summary>
        /// Parses response header
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ResponseHeader ParseHeader(XElement node) {
            var r = new ResponseHeader();
            r.Status = int.Parse(node.XPathSelectElement("int[@name='status']").Value, CultureInfo.InvariantCulture.NumberFormat);
            r.QTime = int.Parse(node.XPathSelectElement("int[@name='QTime']").Value, CultureInfo.InvariantCulture.NumberFormat);
            r.Params = new Dictionary<string, string>();
            var paramNodes = node.XPathSelectElements("lst[@name='params']/str");
            foreach (var n in paramNodes) {
                r.Params[n.Attribute("name").Value] = n.Value;
            }
            return r;
        }

        public ResponseHeader Parse(XDocument response) {
            var responseHeaderNode = response.XPathSelectElement("response/lst[@name='responseHeader']");
            if (responseHeaderNode != null)
                return ParseHeader(responseHeaderNode);
            return null;
        }
    }
}