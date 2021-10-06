using GlobalExceptionHandler.Core.Enums;
using GlobalExceptionHandler.Core.Models;
using GlobalExceptionHandler.Core.Models.ErrorHandling;
using GlobalExceptionHandler.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalExceptionHandler.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            throw new NotAuthorizedException();

            GenericResult result = new GenericResult();            
            result.Data = new RecordModel() { ErrorType = EnumResponseMessageType.Info };
            result.Message = GenericMessages.Success;
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
