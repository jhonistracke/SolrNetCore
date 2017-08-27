using System;
using System.Runtime.Serialization;

namespace SolrNetCore.Exceptions
{
    /// <summary>
    /// Invalid URL specified
    /// </summary>
    [Serializable]
    public class InvalidURLException : SolrNetException
    {
        /// <summary>
        /// Invalid URL specified
        /// </summary>
        /// <param name="innerException"></param>
		public InvalidURLException(Exception innerException) : base(innerException) { }

        /// <summary>
        /// Invalid URL specified
        /// </summary>
        /// <param name="message"></param>
		public InvalidURLException(string message) : base(message) { }

        /// <summary>
        /// Invalid URL specified
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
		public InvalidURLException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Invalid URL specified
        /// </summary>
		public InvalidURLException() { }

        protected InvalidURLException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}