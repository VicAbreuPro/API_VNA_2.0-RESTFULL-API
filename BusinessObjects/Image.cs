namespace API_VNA_2._0.BusinessObjects
{
    public class Image
    {
        public int id { get; set; }

        public string ?name { get; set; }

        public int client_id { get; set; }

        public byte[] ?data { get; set; }

        public Image() { }

    }
}
