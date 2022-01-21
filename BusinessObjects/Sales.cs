using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Sales
    /// </summary>
    [Serializable]
    public class Sales
    {
        public static List<Sale> saleList = new();

        public static List<Sale> SaleList
        {
            get { return saleList; }
        }
    }
}
