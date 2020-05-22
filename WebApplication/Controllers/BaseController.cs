using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{

    [ApiController]
    public class BaseController : ControllerBase
    {

        [NonAction]
        public JsonResult Run<T>(Func<T, JsonResult> func)
        {
            try
            {
                return func(default(T));
            }
            catch
            {
                return null;
            }
        }

    }
}