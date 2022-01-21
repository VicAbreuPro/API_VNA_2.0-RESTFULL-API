using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;
using API_VNA_2._0.BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_VNA_2._0.Controllers
{
    [Route("Sales")]
    [ApiController]
    public class SalesController : Controller
    {
        public static DataSet dt;

        public SalesController()
        {
            dt = Data.DataAccess.AllData();
        }


        [HttpGet("SalesList")]
        public string S_List()
        {
            //Preencher Lista de Produtos com uso do DataSet();
            Sales.saleList = Data.DataAccess.GetSales(Sales.saleList, dt.Tables["vendas"]);
            var json = JsonConvert.SerializeObject(Sales.saleList);
            return json;
        }

        [HttpPost("AddSale")]
        public ActionResult AddSale(string json)
        {
            var sJson = JsonConvert.DeserializeObject<Sale>(json);

            if (sJson != null)
            {
                Sale s = new();

                s.model = sJson.model;
                s.serial = sJson.serial;
                s.valor = sJson.valor;
                s.client_id = sJson.client_id;
                s.date = sJson.date;

                bool aux = Data.DataAccess.AddSale(s);
                if (aux == true) return Ok();
                else return Unauthorized();
            }
            else return Unauthorized();
        }
    }
}
