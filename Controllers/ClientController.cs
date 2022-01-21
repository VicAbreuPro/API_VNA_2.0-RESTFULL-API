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
        public static DataSet dt;

        public ClientController()
        {
            dt = Data.DataAccess.AllData();
        }

        [HttpGet("clientList")]
        public string C_List()
        {
            Clients.clientList = Data.DataAccess.GetClients(Clients.clientList, dt.Tables["clientes"]);
            var json = JsonConvert.SerializeObject(Clients.clientList);
            return json;

        }

        [HttpGet("searchClient/")]
        public string SearchClient(string client_id)
        {
            Client c = new Client();
            Clients.clientList = Data.DataAccess.GetClients(Clients.clientList, dt.Tables["clientes"]);
            // Test List
            foreach (var client in Clients.clientList)
            {
                if (client.id == client_id) c = client;
            }
            var json = JsonConvert.SerializeObject(c);
            return json;
        }

        /// <summary>
        /// Add client to DB by JSON string input
        /// </summary>
        /// /// <param name="json"<></param>
        [HttpPost("AddClient")]
        public ActionResult AddClient(string json)
        {
            var cJson = JsonConvert.DeserializeObject<Client>(json);

            if (cJson != null)
            {
                 Client c = new();

                 c.name = cJson.name;
                 c.id = cJson.id;
                 c.location = cJson.location;
                 c.date = cJson.date;

                bool aux = Data.DataAccess.AddClient(c);
                if(aux == true) return Ok();
                else return Unauthorized();
            }
            else return Unauthorized(); 
        }
    }
}