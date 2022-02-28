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
        public static DataSet ?dt;

        public ClientController()
        {
            dt = Data.DataAccess.AllData();
        }

        [HttpGet("ClientList")]
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

        [HttpPost("UpdateClient")]
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

        [HttpPost("UploadImage")]
        public async Task<ActionResult> UploadClientImage()
        {
            var HttpRequest = Request.Form;
            var file = HttpRequest.Files[0];
            string fileName = file.FileName;

            Image img = new();

            img.name = fileName;
            img.data = new byte[file.Length];

            bool aux = Data.DataAccess.AddImage(img);

            await Task.Delay(2000);

            if (aux == true) return Ok();
            else return Unauthorized();
        }
    }
}