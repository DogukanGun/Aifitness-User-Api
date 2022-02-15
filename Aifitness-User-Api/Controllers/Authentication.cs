using Microsoft.AspNetCore.Mvc;

namespace Aifitness_User_Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class Authentication
    {
        [HttpGet]
        public String getString()
        {
            return "Hello word";
        }
    }
}
