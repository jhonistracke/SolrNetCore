using System;
using System.Runtime.Serialization;

namespace SolrNetCore.Exceptions
{
    /// <summary>
    /// Error connecting to Solr. See inner exception for more information.
    /// </summary>
    [Serializable]
    public class SolrConnectionException : SolrNetException
    {
        private readonly string url;

        public string Url
        {
            get { return url; }
        }

        /// <summary>
        /// Error connecting to Solr.
        /// </summary>
        /// <param name="message"></param>
		public SolrConnectionException(string message) : base(message) { }

        /// <summary>
        /// Error connecting to Solr.
        /// </summary>
        /// <param name="innerException"></param>
		public SolrConnectionException(Exception innerException) : base(innerException) { }

        /// <summary>
        /// Error connecting to Solr.
        /// </summary>
        /// <param name="innerException"></param>
        public SolrConnectionException(Exception innerException, string url) : base(innerException)
        {
            this.url = url;
        }

        /// <summary>
        /// Error connecting to Solr.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
		public SolrConnectionException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Error connecting to Solr.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SolrConnectionException(string message, Exception innerException, string url) : base(message, innerException)
        {
            this.url = url;
        }

        /// <summary>
        /// Error connecting to Solr.
        /// </summary>
		public SolrConnectionException() { }
        protected SolrConnectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}