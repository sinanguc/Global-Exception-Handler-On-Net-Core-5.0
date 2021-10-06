using System;
using System.Net;
using System.Runtime.Serialization;

namespace GlobalExceptionHandler.Core.Models.ErrorHandling
{
    [Serializable]
    public class DatabaseException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.InternalServerError;

        public object Value { get; set; }
        public DatabaseException() : base("Database doesn't work")
        { }

        public DatabaseException(String message) : base(message)
        { }

        public DatabaseException(String message, Exception innerException) : base(message, innerException)
        { }

        protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
