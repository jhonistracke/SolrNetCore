
using System;

namespace SolrNetCore.Impl {
    public interface ISolrQueryByRange {
        string FieldName { get; }

        object From { get; }

        object To { get; }
        
        /// <summary>
        /// Is lower and upper bound inclusive
        /// </summary>
        bool Inclusive { get; }

        /// <summary>
        /// Is lower bound <see cref="From"/> inclusive
        /// ONLY available in Solr 4.0+
        /// </summary>
        bool InclusiveFrom { get; }

        /// <summary>
        /// Is upper bound <see cref="To"/> inclusive
        /// ONLY available in Solr 4.0+
        /// </summary>
        bool InclusiveTo { get; }
    }
}