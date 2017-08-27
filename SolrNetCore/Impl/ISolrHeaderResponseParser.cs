using System.Xml.Linq;

namespace SolrNetCore.Impl {
    public interface ISolrHeaderResponseParser {
        ResponseHeader Parse(XDocument response);
    }
}