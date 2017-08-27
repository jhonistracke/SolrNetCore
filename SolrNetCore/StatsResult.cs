using System.Collections.Generic;

namespace SolrNetCore
{
    /// <summary>
    /// Stats results
    /// <see href="http://wiki.apache.org/solr/StatsComponent"/>
    /// </summary>
    public class StatsResult
    {
        /// <summary>
        /// Minimum value
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// Maximum value
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// Sum of all values
        /// </summary>
        public double Sum { get; set; }

        /// <summary>
        /// How many (non-null) values
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// How many null values
        /// </summary>
        public long Missing { get; set; }

        /// <summary>
        /// Sum of all values squared (useful for stddev)
        /// </summary>
        public double SumOfSquares { get; set; }

        /// <summary>
        /// The average (v1+v2...+vN)/N
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// Standard deviation
        /// </summary>
        public double StdDev { get; set; }

        /// <summary>
        /// Facet results.
        /// <list type="bullet">
        /// <item>Key is the facet field</item>
        /// <item>Value is a dictionary where:
        /// <list type="bullet">
        /// <item>Key is the facet value</item>
        /// <item>Value is the stats for the facet value</item>
        /// </list>
        /// </item>
        /// </list>
        /// </summary>
        public IDictionary<string, Dictionary<string, StatsResult>> FacetResults { get; set; }

        /// <summary>
        /// Stats results
        /// </summary>
        public StatsResult()
        {
            FacetResults = new Dictionary<string, Dictionary<string, StatsResult>>();
        }
    }
}