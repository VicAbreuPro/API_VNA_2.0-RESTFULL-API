
namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Product Detail
    /// </summary>
    [Serializable]
    public class ProductDetail
    {
        public string ?model { get; set; }
        public int ?weight { get; set; }
        public int ?height { get; set; }
        public int ?year { get; set; }
        public string ?color { get; set; }

        public ProductDetail() { } // Default Constructor

        public ProductDetail(string model, int weight, int height, int year, string color)
        {
            this.model = model;
            this.weight = weight;
            this.height = height;
            this.color = color;
            this.year = year;
        }
    }
}
