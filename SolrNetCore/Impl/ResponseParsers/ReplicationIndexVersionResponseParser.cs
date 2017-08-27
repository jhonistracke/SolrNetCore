
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SolrNetCore.Impl.ResponseParsers 
{
    /// <summary>
    /// Parses header (status, QTime, etc), index version and generation from a response
    /// </summary>
    /// <typeparam name="T">Document type</typeparam>
    public class ReplicationIndexVersionResponseParser<T> : ISolrAbstractResponseParser<T>, ISolrReplicationIndexVersionResponseParser
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
        /// <returns>ReplicationIndexVersionResponse class</returns>
        public ReplicationIndexVersionResponse Parse(XDocument response)
        {
            ResponseHeader responseHeader = new ResponseHeader();
            long indexVersion = -1;
            long generation = -1;

            var responseHeaderNode = response.XPathSelectElement("response/lst[@name='responseHeader']");
            if (responseHeaderNode != null)
                responseHeader = ParseHeader(responseHeaderNode);
            else
                return null;

            var responseStatusNode = response.XPathSelectElement("response/long[@name='indexversion']");
            if (responseStatusNode != null)
                indexVersion = long.Parse(responseStatusNode.Value, CultureInfo.InvariantCulture.NumberFormat);

            var responseGenerationNode = response.XPathSelectElement("response/long[@name='generation']");
            if (responseGenerationNode != null)
                generation = long.Parse(responseGenerationNode.Value, CultureInfo.InvariantCulture.NumberFormat);

            return new ReplicationIndexVersionResponse(responseHeader, indexVersion, generation);
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
