using System;
using System.Runtime.Serialization;

namespace SolrNetCore.Exceptions
{
    [Serializable]
    public class TypeNotSupportedException : SolrNetException
    {
        public TypeNotSupportedException(Exception innerException) : base(innerException) { }
        public TypeNotSupportedException(string message) : base(message) { }
        public TypeNotSupportedException() { }
        public TypeNotSupportedException(string message, Exception innerException) : base(message, innerException) { }
        protected TypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}