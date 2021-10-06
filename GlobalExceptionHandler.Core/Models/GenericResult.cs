using GlobalExceptionHandler.Core.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace GlobalExceptionHandler.Core.Models
{
    public class GenericResult
    {
        private readonly DateTime _currentTime;
        public GenericResult()
        {
            _currentTime = DateTime.Now;
        }
        public string Version { get; set; } = "1.0.0";
        public bool Success
        {
            get { return Data != null; }
        }
        private int _statusCode;
        public int StatusCode
        {
            get { return _statusCode; }
            set
            {
                _statusCode = value;
                switch (_statusCode)
                {
                    case (int)HttpStatusCode.Unauthorized:
                    case (int)HttpStatusCode.NotFound:
                    case (int)HttpStatusCode.BadRequest:
                        MessageType = EnumResponseMessageType.Warn.GetHashCode();
                        break;
                    case (int)HttpStatusCode.InternalServerError:
                        MessageType = EnumResponseMessageType.Fatal.GetHashCode();
                        break;
                    default:
                        MessageType = EnumResponseMessageType.Info.GetHashCode();
                        break;
                }
            }
        }

        public int MessageType { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                ExecutionTime = (int)(DateTime.Now - _currentTime).TotalMilliseconds;
            }
        }
        public virtual Object Data { get; set; }
        public string ServerTime
        {
            get { return _currentTime.ToString("dd.MM.yyyy - HH:mm"); }
        }
        public int ExecutionTime { get; set; }

        public class WithValidationErrorMessage : GenericResult
        {
            public List<ValidationErrorModel> ValidationErrors { get; set; }

            public WithValidationErrorMessage()
            {
                ValidationErrors = new List<ValidationErrorModel>();
            }

            public class ValidationErrorModel
            {
                public string FieldName { get; set; }
                public string Message { get; set; }
            }
        }

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
