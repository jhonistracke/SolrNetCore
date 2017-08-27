
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SolrNetCore.Impl.ResponseParsers 
{
    /// <summary>
    /// Parses header (status, QTime, etc) and status from a response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public class ReplicationStatusResponseParser<T> : ISolrAbstractResponseParser<T>, ISolrReplicationStatusResponseParser
    {
        /// <summary>
        /// Header parser
        /// </summary>
        /// <param name="xml">XML</param>
        /// <param name="results">results</param>
        public void Parse(XDocument xml, AbstractSolrQueryResults<T> results) 
        {
            var header = Parse(xml);
            if (header != null)
                results.Header = header.responseHeader;
        }

        /// <summary>
        /// Parses XML response to response class
        /// </summary>
        /// <param name="response">XML</param>
        /// <returns>ReplicationStatusResponse class</returns>
        public ReplicationStatusResponse Parse(XDocument response)
        {
            ResponseHeader responseHeader = new ResponseHeader();
            string status = string.Empty;
            string message = string.Empty;

            var responseHeaderNode = response.XPathSelectElement("response/lst[@name='responseHeader']");
            if (responseHeaderNode != null)
                responseHeader = ParseHeader(responseHeaderNode);
            else
                return null;

            var responseStatusNode = response.XPathSelectElement("response/str[@name='status']");
            if (responseStatusNode != null)
                status = responseStatusNode.Value;
            else
                status = null;

            var responseMessageNode = response.XPathSelectElement("response/str[@name='message']");
            if (responseMessageNode != null)
                message = responseMessageNode.Value;
            else
                message = null;

            return new ReplicationStatusResponse(responseHeader, status, message);
        }

        /// <summary>
        /// Parses response header
        /// </summary>
        /// <param name="node">XML</param>
        /// <returns>ResponseHeader</returns>
        public ResponseHeader ParseHeader(XElement node)
        {
            var r = new ResponseHeader();
            r.Status = int.Parse(node.XPathSelectElement("int[@name='status']").Value, CultureInfo.InvariantCulture.NumberFormat);
            r.QTime = int.Parse(node.XPathSelectElement("int[@name='QTime']").Value, CultureInfo.InvariantCulture.NumberFormat);
            r.Params = new Dictionary<string, string>();

            var paramNodes = node.XPathSelectElements("lst[@name='params']/str");
            foreach (var n in paramNodes)
            {
                r.Params[n.Attribute("name").Value] = n.Value;
            }
            return r;
        }
    }
}
