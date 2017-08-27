

namespace SolrNetCore.Impl {
    /// <summary>
    /// Models a XML node for update consumption
    /// </summary>
    public class PropertyNode {
        /// <summary>
        /// Serialized field value
        /// </summary>
        public string FieldValue { get; set; }

        /// <summary>
        /// Optional field name suffix
        /// </summary>
        public string FieldNameSuffix { get; set; }
    }
}