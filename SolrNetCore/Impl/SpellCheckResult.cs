
using System.Collections.Generic;

namespace SolrNetCore.Impl {
    /// <summary>
    /// Spell-checking results
    /// </summary>
    public class SpellCheckResult {
        /// <summary>
        /// Original query term
        /// </summary>
        public string Query { get; set;}

        /// <summary>
        /// Result count for original term
        /// </summary>
        public int NumFound { get; set;}

        /// <summary>
        /// Start offset
        /// </summary>
        public int StartOffset { get; set;}

        /// <summary>
        /// End offset
        /// </summary>
        public int EndOffset { get; set;}

        /// <summary>
        /// Spelling suggestions
        /// </summary>
        public ICollection<string> Suggestions { get; set;}
    }
}