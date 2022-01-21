using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Producs Detail
    /// </summary>
    [Serializable]
    public class ProductDetails
    {
        public static List<ProductDetail> productDetailList = new();

        public static List<ProductDetail> ProductDetailList
        {
            get { return productDetailList; }
        }
    }
}
