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
        public ActionResult<string> P_List()
        {
            List<Product> list = Products.GetProducts();
            if(list != null) return Ok(JsonConvert.SerializeObject(list));
            else return Unauthorized("Failed to fetch");
        }

        [HttpGet("TopStockProduct")]
        public ActionResult<string> Tp_Product()
        {
            string? topProduct = Products.TopProductStock();
            if (topProduct != null) return Ok(topProduct);
            return Unauthorized();
        }

        [HttpPost("AddProduct")]
        public ActionResult<string> PostProduct(Product p)
        {
            // Add new Product with bool response to confirm the success of operation
            bool aux = Products.AddProduct(p);

            // Return Http code according the result of Add Product Method from data layer
            if (aux == true) return Ok("succes");
            else return Unauthorized("failed to add product");
        }

        [HttpPut("UpdateProduct")]
        public ActionResult<string> UpdateProduct(Product p)
        {
            // Update Porduct with bool response to confirm the success of operation
            bool aux = Products.UpdateProduct(p);

            // Return Http code according the result of Update Product Method from data layer
            if (aux == true) return Ok("succes");
            else return Unauthorized("failed to update");
        }
    }
}
