using Microsoft.AspNetCore.Mvc;
using API_VNA_2._0.BusinessObjects;
using Newtonsoft.Json;

namespace API_VNA_2._0.Controllers
{
    [Route("Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet("ClientList")]
        public ActionResult<string> C_List()
        {
            List<Client> list = Clients.GetClients();
            if (list != null) return Ok(JsonConvert.SerializeObject(list));
            else return Unauthorized("Failed to fetch");
        }

        [HttpGet("TopClientLocation")]
        public ActionResult<string> TopCliLocal()
        {
            string? topLocal = Clients.TopClientLocation();
            if(topLocal != null) return Ok(topLocal.ToString());
            else return Unauthorized("failed to fetch");
        }

        [HttpPost("AddClient")]
        public ActionResult<string> PostClient(Client c)
        {
            // Add new Client with bool response to confirm the success of operation
            bool aux = Clients.AddClient(c);

            // Return Http code according the result of Add Client Method from data layer
            if(aux == true) return Ok("success");
            else return Unauthorized("failed to add client");
        }

        [HttpPut("UpdateClient")]
        public ActionResult<string> UpdateClient(Client c)
        {
            // Update Client with bool response to confirm the success of operation
            bool aux = Clients.UpdateClient(c);

            // Return Http code according the result of Update Client Method from data layer
            if (aux == true) return Ok("succes");
            else return Unauthorized("update failed");
        }
    }
}