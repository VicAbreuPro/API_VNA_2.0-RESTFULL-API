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
        public static DataSet dt;

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
        public ActionResult AddProduct(string json)
        {
            var pJson = JsonConvert.DeserializeObject<Product>(json);

            if (pJson != null)
            {
                Product p = new Product();

                p.model = pJson.model;
                p.serial = pJson.serial;
                p.valor = pJson.valor;

                bool aux = Data.DataAccess.AddProduct(p);
                if (aux == true) return Ok();
                else return Unauthorized();
            }
            else return Unauthorized();
        }

    }
}
