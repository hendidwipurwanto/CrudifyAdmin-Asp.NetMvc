using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/Server")]
        public IActionResult Server()
        {
            // Bisa juga log error-nya di sini
            return View("ServerError");
        }

        [Route("Error/HttpStatus")]
        public IActionResult HttpStatus(int code)
        {
            ViewBag.StatusCode = code;

            // Bisa custom tampilan per status code
            switch (code)
            {
                case 401:
                    return View("Unauthorized");
                case 403:
                    return View("Forbidden");
                case 404:
                    return View("NotFound");
                default: //error 500
                    return View("ServerError");
            }
        }
    }

}
