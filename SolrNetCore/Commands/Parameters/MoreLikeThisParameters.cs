using System.Collections.Generic;

namespace SolrNetCore.Commands.Parameters
{
    /// <summary>
    /// More-like-this parameters
    /// See http://wiki.apache.org/solr/MoreLikeThis
    /// </summary>
    public class MoreLikeThisParameters
    {
        private readonly IEnumerable<string> fields;

        /// <summary>
        /// Creates more-like-this parameters
        /// </summary>
        /// <param name="fields">The fields to use for similarity. </param>
        public MoreLikeThisParameters(IEnumerable<string> fields)
        {
            this.fields = fields;
        }

        /// <summary>
        /// The fields to use for similarity. 
        /// NOTE: if possible, these should have a stored TermVector
        /// </summary>
        public IEnumerable<string> Fields
        {
            get { return fields; }
        }

        /// <summary>
        /// Minimum Term Frequency - the frequency below which terms will be ignored in the source doc.
        /// </summary>
        public int? MinTermFreq { get; set; }

        /// <summary>
        /// Minimum Document Frequency - the frequency at which words will be ignored which do not occur in at least this many docs.
        /// </summary>
        public int? MinDocFreq { get; set; }

        /// <summary>
        /// Minimum word length below which words will be ignored.
        /// </summary>
        public int? MinWordLength { get; set; }

        /// <summary>
        /// Maximum word length above which words will be ignored.
        /// </summary>
        public int? MaxWordLength { get; set; }

        /// <summary>
        /// Maximum number of query terms that will be included in any generated query.
        /// </summary>
        public int? MaxQueryTerms { get; set; }

        /// <summary>
        /// Maximum number of tokens to parse in each example doc field that is not stored with TermVector support.
        /// </summary>
        public int? MaxTokens { get; set; }

        /// <summary>
        /// Set if the query will be boosted by the interesting term relevance.
        /// </summary>
        public bool? Boost { get; set; }

        /// <summary>
        /// Query fields and their boosts using the same format as that used in DisMaxRequestHandler. 
        /// These fields must also be specified in <see cref="Fields"/>
        /// </summary>
        public ICollection<string> QueryFields { get; set; }

        /// <summary>
        /// The number of similar documents to return for each result.
        /// </summary>
        public int? Count { get; set; }
    }
}