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
        public static DataSet ?dt;

        public ProductController()
        {
            dt = Data.DataAccess.AllData();
        }

        [HttpGet("ProductList")]
        public string P_List()
        {
            //Preencher Lista de Produtos com uso do DataSet();
            Products.productList = Data.DataAccess.GetProducts(Products.productList, dt.Tables["produtos"]);
            var json = JsonConvert.SerializeObject(Products.productList);
            return json;
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

        [HttpPost("UpdateProduct")]
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
