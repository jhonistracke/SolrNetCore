using System.Xml.Linq;

namespace SolrNetCore
{
    /// <summary>
    /// Serializes a solr document to xml. 
    /// </summary>
    /// <typeparam name="T">document type</typeparam>
    public interface ISolrDocumentSerializer<in T>
    {
        /// <summary>
        /// Serializes a Solr document to xml, applying an index-time boost
        /// </summary>
        /// <param name="doc">document to serialize</param>
        /// <param name="boost"></param>
        /// <returns>serialized document</returns>
        XElement Serialize(T doc, double? boost);
    }
}