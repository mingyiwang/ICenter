using Microsoft.AspNetCore.Mvc;
using WebAPI.Transfer;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/{controller}/{action}")]
    public class LogInServiceXController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Home()
        {
            return "Welcome";
        }

        [HttpPost]
        public JsonResult LogIn()
        {
            return new JsonResult(new Session
            {
                passport = "12312323"
            });
        }

    }

}
