using GlobalExceptionHandler.Core.Enums;
using GlobalExceptionHandler.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using static GlobalExceptionHandler.Core.Models.GenericResult.WithValidationErrorMessage;

namespace GlobalExceptionHandler.Core.Aspects
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var errorsInModelState = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            GenericResult.WithValidationErrorMessage errorReponse = new GenericResult.WithValidationErrorMessage();

            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {
                    ValidationErrorModel errorModel = new ValidationErrorModel
                    {
                        FieldName = error.Key,
                        Message = subError
                    };

                    errorReponse.ValidationErrors.Add(errorModel);
                }
            }

            errorReponse.Message = GenericMessages.Please_fill_in_all_required_fields;
            errorReponse.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
            context.HttpContext.Response.StatusCode = errorReponse.StatusCode;
            context.Result = new JsonResult(errorReponse) { StatusCode = HttpStatusCode.BadRequest.GetHashCode() };
        }
    }
}
