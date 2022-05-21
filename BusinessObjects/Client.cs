

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Client
    /// </summary>
    [Serializable]
    public class Client
    {
        public string ?id { get; set; }
        public string ?name { get; set; }
        public string ?location { get; set; }
        public string ?date { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Client() { }

        /// <summary>
        /// Client Constructor
        /// </summary>
        public Client(string id, string name, string location, string date)
        {
            this.id = id;
            this.name = name;
            this.location = location;
            this.date = date;
        }
    }
}

