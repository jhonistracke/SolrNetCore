using System;

namespace SolrNetCore
{
    /// <summary>
    /// Field collapsing result
    /// </summary>
    [Obsolete("Use result grouping instead")]
    public class CollapsedDocument
    {
        ///<summary>
        /// Document ID
        ///</summary>
        public string Id { get; set; }

        ///<summary>
        /// Collapsed Field Value
        ///</summary>
        public string FieldValue { get; set; }

        /// <summary>
        /// Collapsed field count
        /// </summary>
        public int CollapseCount { get; set; }
    }
}