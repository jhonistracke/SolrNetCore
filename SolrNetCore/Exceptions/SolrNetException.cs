using System;
using System.Runtime.Serialization;

namespace SolrNetCore.Exceptions
{
    /// <summary>
    /// Base exception for all exceptions thrown by SolrNet
    /// </summary>
    [Serializable]
    public class SolrNetException : ApplicationException
    {
        /// <summary>
        /// Base exception for all exceptions thrown by SolrNet
        /// </summary>
        /// <param name="innerException"></param>
        public SolrNetException(Exception innerException) : base(innerException.Message, innerException) { }

        /// <summary>
        /// Base exception for all exceptions thrown by SolrNet
        /// </summary>
        /// <param name="message"></param>
		public SolrNetException(string message) : base(message) { }

        /// <summary>
        /// Base exception for all exceptions thrown by SolrNet
        /// </summary>
		public SolrNetException() { }

        /// <summary>
        /// Base exception for all exceptions thrown by SolrNet
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
		public SolrNetException(string message, Exception innerException) : base(message, innerException) { }
        protected SolrNetException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}