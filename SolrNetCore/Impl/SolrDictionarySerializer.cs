
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Serializes a dictionary document
    /// </summary>
    public class SolrDictionarySerializer : ISolrDocumentSerializer<Dictionary<string, object>> {
        private readonly ISolrFieldSerializer serializer;

        public SolrDictionarySerializer(ISolrFieldSerializer serializer) {
            this.serializer = serializer;
        }

        public XElement Serialize(Dictionary<string, object> doc, double? boost) {
            if (doc == null)
                throw new ArgumentNullException("doc");
            var docNode = new XElement("doc");
            if (boost.HasValue) {
                var boostAttr = new XAttribute("boost", boost.Value.ToString(CultureInfo.InvariantCulture.NumberFormat));
                docNode.Add(boostAttr);
            }
            foreach (var kv in doc) {
                var nodes = serializer.Serialize(kv.Value);
                foreach (var n in nodes) {
                    var value = SolrDocumentSerializer<object>.RemoveControlCharacters(n.FieldValue);
                    if (value != null) {
                        var fieldNode = new XElement("field", new XAttribute("name", kv.Key), value);
                        docNode.Add(fieldNode);
                    }
                }
            }
            return docNode;
        }
    }
}
