using SolrNetCore.Impl;

namespace SolrNetCore
{
    /// <summary>
    /// Queries a field for a range
    /// </summary>
    /// <typeparam name="RT"></typeparam>
    public class SolrQueryByRange<RT> : AbstractSolrQuery, ISolrQueryByRange
    {
        private readonly string fieldName;
        private readonly RT from;
        private readonly RT to;
        private readonly bool inclusiveFrom;
        private readonly bool inclusiveTo;

        /// <summary>
        /// Creates an range query with inclusive bounds
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public SolrQueryByRange(string fieldName, RT from, RT to) : this(fieldName, from, to, true) { }

        /// <summary>
        /// Creates a range query
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="inclusive">Inclusive bounds</param>
        public SolrQueryByRange(string fieldName, RT @from, RT to, bool inclusive) : this(fieldName, from, to, inclusive, inclusive) { }

        /// <summary>
        /// Creates a range query.
        /// Different bounds inclusiveness ONLY available in Solr 4.0+
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="inclusiveFrom">Lower bound inclusive</param>
        /// <param name="inclusiveTo">Upper bound inclusive</param>
        public SolrQueryByRange(string fieldName, RT @from, RT to, bool inclusiveFrom, bool inclusiveTo)
        {
            this.fieldName = fieldName;
            this.from = from;
            this.to = to;
            this.inclusiveFrom = inclusiveFrom;
            this.inclusiveTo = inclusiveTo;
        }

        public string FieldName
        {
            get { return fieldName; }
        }

        public RT From
        {
            get { return from; }
        }

        object ISolrQueryByRange.From
        {
            get { return from; }
        }

        public RT To
        {
            get { return to; }
        }

        object ISolrQueryByRange.To
        {
            get { return to; }
        }

        /// <summary>
        /// Is lower and upper bound inclusive
        /// </summary>
        public bool Inclusive
        {
            get { return inclusiveFrom && inclusiveTo; }
        }

        /// <summary>
        /// Is lower bound <see cref="From"/> inclusive
        /// </summary>
        public bool InclusiveFrom
        {
            get { return inclusiveFrom; }
        }

        /// <summary>
        /// Is upper bound <see cref="To"/> inclusive
        /// </summary>
        public bool InclusiveTo
        {
            get { return inclusiveTo; }
        }

    }
}