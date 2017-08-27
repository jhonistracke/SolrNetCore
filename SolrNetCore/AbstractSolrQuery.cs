using System;

namespace SolrNetCore
{
    /// <summary>
    /// Abstract Solr query, used to define operator overloading
    /// </summary>
    public abstract class AbstractSolrQuery : ISolrQuery
    {
        /// <summary>
        /// Negates this query
        /// </summary>
        /// <returns></returns>
        public AbstractSolrQuery Not()
        {
            return new SolrNotQuery(this);
        }

        /// <summary>
        /// Negates this query
        /// </summary>
        /// <returns></returns>
        public AbstractSolrQuery Required()
        {
            return new SolrRequiredQuery(this);
        }

        /// <summary>
        /// Boosts this query
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public AbstractSolrQuery Boost(double factor)
        {
            return new SolrQueryBoost(this, factor);
        }

        public static AbstractSolrQuery operator &(AbstractSolrQuery a, AbstractSolrQuery b)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            if (b == null)
                throw new ArgumentNullException("b");
            return new SolrMultipleCriteriaQuery(new[] { a, b }, "AND");
        }

        public static AbstractSolrQuery operator |(AbstractSolrQuery a, AbstractSolrQuery b)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            if (b == null)
                throw new ArgumentNullException("b");
            return new SolrMultipleCriteriaQuery(new[] { a, b }, "OR");
        }

        public static AbstractSolrQuery operator +(AbstractSolrQuery a, AbstractSolrQuery b)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            if (b == null)
                throw new ArgumentNullException("b");
            return new SolrMultipleCriteriaQuery(new[] { a, b });
        }

        public static AbstractSolrQuery operator -(AbstractSolrQuery a, AbstractSolrQuery b)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            if (b == null)
                throw new ArgumentNullException("b");
            return new SolrMultipleCriteriaQuery(new[] { a, b.Not() });
        }

        public static bool operator false(AbstractSolrQuery a)
        {
            return false;
        }

        public static bool operator true(AbstractSolrQuery a)
        {
            return false;
        }

        public static AbstractSolrQuery operator !(AbstractSolrQuery a)
        {
            if (a == null)
                throw new ArgumentNullException("a");
            return a.Not();
        }
    }
}