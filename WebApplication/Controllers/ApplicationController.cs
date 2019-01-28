using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : BaseController
    {

        [HttpGet]
        public JsonResult Get()
        {
            return Run<Pojo> (element => new JsonResult(new Pojo()));
        }


    }

    public class Pojo
    {
        public int a { get; set; }
        public int b { get; set; }

        public Pojo()
        {
            a = 1;
            b = 2;
        }
    }
}