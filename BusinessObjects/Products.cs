
namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Porducts
    /// </summary>
    [Serializable]
    public class Products
    {
        public static List<Product> productList = new();

        public static List<Product> ProductList
        {
            get { return productList; }
        }
    }
}
