using GlobalExceptionHandler.Core.Enums;
using GlobalExceptionHandler.Core.Models;
using GlobalExceptionHandler.Core.Models.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionHandler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<GenericResult> Get([FromQuery] RecordModel model)
        {
            if (model.ErrorType == EnumResponseMessageType.Error)
                throw new DatabaseException();

            GenericResult result = new GenericResult();
            result.Data = model;
            result.Message = GenericMessages.Success;
            return result;
        }
    }
}
