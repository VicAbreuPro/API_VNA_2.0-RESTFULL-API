namespace API_VNA_2._0.BusinessObjects
{
    [Serializable]
    public class User_tk
    {
        public string? id { get; set; }
        public int type { get; set; }
        public string? token { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public User_tk() { }

        public User_tk(string? id_inp, int type_inp, string? toke_inp)
        {
            this.id = id_inp;
            this.type = type_inp;
            this.token = toke_inp;
        }
    }
}
