
using System;
using System.Collections.Generic;

namespace SolrNetCore
{
    /// <summary>
    /// Collapse results
    /// <see href="https://issues.apache.org/jira/browse/SOLR-236"/>
    /// </summary>
    [Obsolete("Use result grouping instead")]
    public class CollapseResults
    {

        /// <summary>
        /// &amp;collapse.field=
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Collapsed document.ids and their counts
        /// </summary>
        public ICollection<CollapsedDocument> CollapsedDocuments { get; set; }

        ///<summary>
        /// Initializer
        ///</summary>
        public CollapseResults()
        {
            CollapsedDocuments = new List<CollapsedDocument>();
        }
    }
}