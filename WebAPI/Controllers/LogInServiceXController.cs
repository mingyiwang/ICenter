using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    public class LogInServiceXController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Home()
        {
            return "Welcome";
        }

        [HttpPost]
        public JsonResult LogIn(string userName, string password)
        {
            return new JsonResult("sdkfjdlfjdlfj");
        }

    }

}
