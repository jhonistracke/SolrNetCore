using System;
using System.Collections.Generic;

namespace SolrNetCore
{
    /// <summary>
    /// Date faceting result
    /// </summary>
    public class DateFacetingResult
    {

        /// <summary>
        /// Date range gap (e.g. "+1DAY")
        /// </summary>
        public string Gap { get; set; }

        /// <summary>
        /// Maximum value
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// The date faceting results.
        /// </summary>
        public IList<KeyValuePair<DateTime, int>> DateResults { get; set; }

        /// <summary>
        /// Other date faceting results.
        /// </summary>
        public IDictionary<FacetDateOther, int> OtherResults { get; set; }

        /// <summary>
        /// Date faceting result
        /// </summary>
		public DateFacetingResult()
        {
            DateResults = new List<KeyValuePair<DateTime, int>>();
            OtherResults = new Dictionary<FacetDateOther, int>();
        }
    }
}