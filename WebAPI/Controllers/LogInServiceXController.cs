using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   
    [Route("api/[controller]/[action]")]
    public class LogInServiceXController : Controller
    {

        [HttpGet]
        public JsonResult Home()
        {
            return new JsonResult("Welcome");
        }

        [HttpPost]
        public JsonResult LogIn(string userName, string password)
        {
            return new JsonResult("sdkfjdlfjdlfj");
        }

    }

}
