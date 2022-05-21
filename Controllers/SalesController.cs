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
        public ActionResult<string> S_List()
        {
            List<Sales_view> list = Sales_view.GetSales();
            if(list != null) return Ok(JsonConvert.SerializeObject(list));
            else return Unauthorized("faile to fecth");
        }

        [HttpGet("TopSaleProduct")]
        public ActionResult<string> TopSaleP()
        {
            string? result = Sales_view.TopSalesProduct();
            if(result != null) return Ok(result);
            else return Unauthorized();
        }

        [HttpGet("YearAmount")]
        public ActionResult<string> YearSalesAmount()
        {
            // Get Sales Amount from current year
            var aux = Sales_view.YearlySale();

            // Return result
            if (aux != 0) return Ok(aux.ToString());
            else return Unauthorized(aux.ToString());
        }

        [HttpPost("AddSale")]
        public ActionResult<string> PostSale(Sale s)
        {
            // Add new Sale with bool response to confirm the success of operation
            bool aux = Sale.AddSale(s);

            // Return Http code according the result of Add Sale Method from data layer
            if (aux == true) return Ok("success");
            else return Unauthorized("failed to add sale");
        }

        [HttpPut("UpdateSale")]
        public ActionResult<string> UpdateSale(Sale s)
        {
            // Update Sale with bool response to confirm the success of operation
            bool aux = Sale.UpdateSale(s);

            // Return Http code according the result of Update Sale Method from data layer
            if (aux == true) return Ok("success");
            else return Unauthorized("failed to update");
        }
    }
}
