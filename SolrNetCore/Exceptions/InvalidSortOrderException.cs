using System;
using System.Runtime.Serialization;

namespace SolrNetCore.Exceptions
{
    /// <summary>
    /// Error parsing <see cref="SortOrder"/>
    /// </summary>
    [Serializable]
    public class InvalidSortOrderException : SolrNetException
    {
        public InvalidSortOrderException() { }
        public InvalidSortOrderException(string message) : base(message) { }
        public InvalidSortOrderException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidSortOrderException(Exception innerException) : base(innerException) { }
        protected InvalidSortOrderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}