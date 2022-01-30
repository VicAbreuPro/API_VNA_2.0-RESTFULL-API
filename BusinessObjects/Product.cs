
namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Product
    /// </summary>
    [Serializable]
    public class Product
    {
        public int ?serial { get; set; }
        public int ?valor { get; set; }
        public string ?model { get; set; }

        public Product()
        {
            // Class Deafult Constructor
        }

        public Product(int serial, string model, int valor)
        {
            this.serial = serial;
            this.model = model;
            this.valor = valor;
        }
    }
}
