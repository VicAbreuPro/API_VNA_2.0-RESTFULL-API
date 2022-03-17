using Microsoft.AspNetCore.Mvc;

namespace API_VNA_2._0.Controllers
{
    [Route("Internal")]
    [ApiController]
    public class InternalController : Controller
    {
        /// <summary>
        /// Verify User and Return type
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("UserType/{token}")]
        public ActionResult<string> UserType(string token)
        {
            int type = Data.DataAccess.VerifyUser(token);
            if(type != 0) return Ok(type.ToString());
            else return NotFound(type);
        }

    }
}
