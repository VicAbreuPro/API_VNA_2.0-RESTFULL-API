using Microsoft.AspNetCore.Mvc;
using System.Data;
using API_VNA_2._0.BusinessObjects;
using Newtonsoft.Json;

namespace API_VNA_2._0.Controllers
{
    [Route("Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet("ClientList")]
        public string C_List()
        {
            Clients.clientList = Data.DataAccess.GetClients();
            var json = JsonConvert.SerializeObject(Clients.clientList);
            return json;
        }

        [HttpGet("TopClientLocation")]
        public ActionResult<string> TopCliLocal()
        {
            string topLocal = Clients.TopClientLocation();
            if(topLocal != null) return Ok(topLocal);
           else return Unauthorized();
        }

        [HttpPost("AddClient")]
        public async Task<ActionResult> PostClient(Client c)
        {
            // Add new Client with bool response to confirm the success of operation
            bool aux = Data.DataAccess.AddClient(c);

            // Define Task Delay
            await Task.Delay(2000);

            // Return Http code according the result of Add Client Method from data layer
            if(aux == true) return Ok();
            else return Unauthorized();
        }

        [HttpPut("UpdateClient")]
        public async Task<ActionResult> UpdateClient(Client c)
        {
            // Update Client with bool response to confirm the success of operation
            bool aux = Data.DataAccess.UpdateClient(c);

            // Define Task Delay
            await Task.Delay(2000);

            // Return Http code according the result of Update Client Method from data layer
            if (aux == true) return Ok();
            else return Unauthorized();
        }
    }
}