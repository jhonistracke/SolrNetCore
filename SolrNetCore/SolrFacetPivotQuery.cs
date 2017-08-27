using System.Collections.Generic;

namespace SolrNetCore
{
    /// <summary>
    /// pivot facet query
    /// <see href="http://wiki.apache.org/solr/SimpleFacetParameters#facet.pivot"/>
    /// </summary>
    public class SolrFacetPivotQuery : ISolrFacetQuery
    {
        /// <summary>
        /// A list of fields to pivot. Multiple values will create multiple sections in the response.
        /// <example>
        /// Example single pivot value  new []{"manu,cat"}
        /// Example multiple pivot values :  new [] {"manu,cat","inStock,cat"}
        /// </example>
        /// </summary>

        public ICollection<PivotFields> Fields { get; set; }

        /// <summary>
        /// The minimum number of documents that need to match for the result to show up in the results. Default value is 1
        /// </summary>
        public int? MinCount { get; set; }
    }
}
