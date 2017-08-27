using System.Xml.Linq;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Parses the extract response
    /// </summary>
    public interface ISolrExtractResponseParser {
        ExtractResponse Parse(XDocument response);
    }
}