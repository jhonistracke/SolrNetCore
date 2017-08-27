using System;
using System.Runtime.Serialization;

namespace SolrNetCore.Exceptions
{
    /// <summary>
    /// Solr did not understand one the specified fields
    /// </summary>
    [Serializable]
    [Obsolete("No longer thrown, catch SolrNetException or SolrConnectionException instead")]
    public class InvalidFieldException : SolrNetException
    {
        /// <summary>
        /// Solr did not understand one the specified fields
        /// </summary>
        /// <param name="innerException"></param>
		public InvalidFieldException(Exception innerException) : base(innerException) { }

        /// <summary>
        /// Solr did not understand one the specified fields
        /// </summary>
        /// <param name="message"></param>
		public InvalidFieldException(string message) : base(message) { }

        /// <summary>
        /// Solr did not understand one the specified fields
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
		public InvalidFieldException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Solr did not understand one the specified fields
        /// </summary>
		public InvalidFieldException() { }
        protected InvalidFieldException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}