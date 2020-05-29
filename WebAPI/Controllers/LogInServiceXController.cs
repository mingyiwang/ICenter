using Microsoft.AspNetCore.Mvc;
using WebAPI.Transfer;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/LogInServiceX")]
    public class LogInServiceXController : ControllerBase
    {

        [HttpPost]
        public JsonResult LogIn(string userName, string password)
        {
            return new JsonResult(new Session
            {
                passport = "12312323"
            });
        }

    }

}
