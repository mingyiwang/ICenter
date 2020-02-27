using Core.Json;
using EL.Persist;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {

        public void Authenticate(User user)
        {
            //1. find user from database.
            //2. compare user name and password.
            //3. return user data
            JsonUtils.Serialize(user);
        }

    }

}
