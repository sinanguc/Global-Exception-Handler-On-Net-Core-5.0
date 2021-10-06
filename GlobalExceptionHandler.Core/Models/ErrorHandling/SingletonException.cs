using System;
using System.Net;
using System.Runtime.Serialization;

namespace GlobalExceptionHandler.Core.Models.ErrorHandling
{
    [Serializable]
    public class SingletonException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.InternalServerError;

        public object Value { get; set; }
        public SingletonException() : base()
        { }

        public SingletonException(String message) : base(message)
        { }

        public SingletonException(String message, Exception innerException) : base(message, innerException)
        { }

        protected SingletonException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
