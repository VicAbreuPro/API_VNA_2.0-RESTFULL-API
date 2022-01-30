
namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Sale
    /// </summary>
    [Serializable]
    public class Sale
    {
        public string ?sale_id { get; set; }
        public int ?serial { get; set; }
        public int ?valor { get; set; }
        public string ?model { get; set; }
        public int ?client_id { get; set; }
        public string ?date { get; set; }

        public Sale()
        {
            // Class Deafult Constructor
        }

        public Sale(string sale_id, int client_id, int serial, string model, string date, int valor)
        {
            this.sale_id = sale_id;
            this.serial = serial;
            this.model = model;
            this.client_id = client_id;
            this.date = date;
            this.valor = valor;
        }
    }
}
