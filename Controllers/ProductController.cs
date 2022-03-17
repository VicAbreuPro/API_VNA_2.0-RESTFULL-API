using Microsoft.AspNetCore.Mvc;
using System.Data;
using API_VNA_2._0.BusinessObjects;
using Newtonsoft.Json;

namespace API_VNA_2._0.Controllers
{
    [ApiController]
    [Route("Product")]
    public class ProductController : ControllerBase
    {
        [HttpGet("ProductList")]
        public string P_List()
        {
            Products.productList = Data.DataAccess.GetProducts();
            var json = JsonConvert.SerializeObject(Products.productList);
            return json;
        }

        [HttpGet("TopStockProduct")]
        public ActionResult<string> Tp_Product()
        {
            string topProduct = Products.TopProductStock();
            if (topProduct != null) return Ok(topProduct);
            return Unauthorized();
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult> PostProduct(Product p)
        {
            // Add new Product with bool response to confirm the success of operation
            bool aux = Data.DataAccess.AddProduct(p);

            // Define Task Delay
            await Task.Delay(2000);

            // Return Http code according the result of Add Product Method from data layer
            if (aux == true) return Ok();
            else return Unauthorized();
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct(Product p)
        {
            // Update Porduct with bool response to confirm the success of operation
            bool aux = Data.DataAccess.UpdateProduct(p);

            // Define Task Delay
            await Task.Delay(2000);

            // Return Http code according the result of Update Product Method from data layer
            if (aux == true) return Ok();
            else return Unauthorized();
        }
    }
}
