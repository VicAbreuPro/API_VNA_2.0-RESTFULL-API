using Microsoft.AspNetCore.Mvc;
using System.Data;
using API_VNA_2._0.BusinessObjects;
using Newtonsoft.Json;

namespace API_VNA_2._0.Controllers
{
    [Route("Sales")]
    [ApiController]
    public class SalesController : Controller
    {
        [HttpGet("SalesList")]
        public string S_List()
        {
            Sales.saleList = Data.DataAccess.GetSales();
            var json = JsonConvert.SerializeObject(Sales.saleList);
            return json;
        }

        [HttpPost("AddSale")]
        public async Task<ActionResult> PostSale(Sale s)
        {
            // Add new Sale with bool response to confirm the success of operation
            bool aux = Data.DataAccess.AddSale(s);

            // Define Task Delay
            await Task.Delay(2000);

            // Return Http code according the result of Add Sale Method from data layer
            if (aux == true) return Ok();
            else return Unauthorized();
        }

        [HttpPut("UpdateSale")]
        public async Task<ActionResult> UpdateSale(Sale s)
        {
            // Update Sale with bool response to confirm the success of operation
            bool aux = Data.DataAccess.UpdateSale(s);

            // Define Task Delay
            await Task.Delay(2000);

            // Return Http code according the result of Update Sale Method from data layer
            if (aux == true) return Ok();
            else return Unauthorized();
        }

    }
}
