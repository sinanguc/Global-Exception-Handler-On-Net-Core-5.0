using GlobalExceptionHandler.Core.Models;
using GlobalExceptionHandler.Core.Models.ErrorHandling;
using GlobalExceptionHandler.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace GlobalExceptionHandler.Core.Aspects
{
    public class WebApiHttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null) // if request is return exception
                return;
            
            GenericResult result = new GenericResult();
            result.Message = context.Exception.Message;
            result.StatusCode = HttpStatusCode.InternalServerError.GetHashCode(); // if it doesn't match the error types

            switch (context.Exception)
            {
                case LoginIncorrectException:
                case TokenException:
                    result.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
                    break;
                case RecordExistException:
                    result.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    break;
                case ValidationException:
                case ArgumentNullException:
                case RecordNotFoundException:
                    result.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                    break;
                case DatabaseException:
                    result.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                    break;
                default:
                    result.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                    break;
            }

            // You can record exception log on here
            LogHelper.WriteLog(context.Exception);

            context.ExceptionHandled = true;

            context.HttpContext.Response.StatusCode = result.StatusCode;
            context.HttpContext.Response.ContentType = "application/problem+json";
            context.Result = new JsonResult(result); // return GenericResult model to current View as Json
        }
    }
}
