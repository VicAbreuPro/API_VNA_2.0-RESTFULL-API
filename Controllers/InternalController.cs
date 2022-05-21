using Microsoft.AspNetCore.Mvc;
using API_VNA_2._0.BusinessObjects;
using API_VNA_2._0.Data;
using Newtonsoft.Json;

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
            int type = DataAccess.VerifyUser(token);
            if(type != 0) return Ok(type.ToString());
            else return NotFound(type.ToString());
        }

        /// <summary>
        /// Verify User and Return type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("Get_users")]
        public ActionResult<string> Get_users(string request)
        {
            if(request == "request.data.users.code_1")
            {
                List<User_aux> list = User_aux.Get_user_list();
                if (list != null) return Ok(JsonConvert.SerializeObject(list));
                else return Unauthorized("failed");
            }else return Unauthorized("failed");
            
        }

        /// <summary>
        /// Verify User and Return type
        /// </summary>
        /// <returns></returns>
        [HttpGet("Total_users")]
        public ActionResult<string> Total_users()
        {
            int total = User_aux.Total_u(0);
            if (total != 0) return Ok(total.ToString());
            else return NotFound(total.ToString());
        }

        /// <summary>
        /// Verify User and Return type
        /// </summary>
        /// <returns></returns>
        [HttpGet("Total_admins")]
        public ActionResult<string> Total_admin()
        {
            int total = User_aux.Total_u(1);
            if (total != 0) return Ok(total.ToString());
            else return NotFound(total.ToString());
        }

        /// <summary>
        /// Verify User and Return type
        /// </summary>
        /// <returns></returns>
        [HttpGet("Last_entry")]
        public ActionResult<string> Last_entry()
        {
            string? username = User_aux.Get_last();
            if (username != "") return Ok(username);
            else return NotFound(username);
        }

        /// <summary>
        /// Verify User and Return type
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public ActionResult<string> Login_user(User_aux u)
        {
            bool resp = User_aux.Login(u);
            if (resp != false) return Ok("allow");
            else return Unauthorized("deny");
        }
    }
}
