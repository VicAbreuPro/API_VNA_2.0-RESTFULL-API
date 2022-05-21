using Microsoft.AspNetCore.Mvc;
using API_VNA_2._0.BusinessObjects;

namespace API_VNA_2._0.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("test_aux")]
        public async Task<ActionResult> Test_post()
        {
            int count = 0;
            List<Sale_Aux> sale_aux = Data.DataAccess.Get_sale_Aux();

            foreach(var sale in sale_aux)
            {
                sale.cli_id = Data.DataAccess.get_id_sales_aux(sale.name);
            }

            foreach(var sale in sale_aux)
            {
                bool aux = Data.DataAccess.Add_Test(sale);
                if (aux != false) count += 1;

            }
            
            // Define Task Delay
            await Task.Delay(1500);

            // Return Http code according the result of Add Client Method from data layer
            return Ok(count.ToString());

        }
    }
}
