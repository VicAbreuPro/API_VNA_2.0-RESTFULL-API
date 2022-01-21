using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_VNA_2._0.BusinessObjects
{
    /// <summary>
    /// Serializable object Business Clients
    /// </summary>
    [Serializable]
    public class Clients
    {
        public static List<Client> clientList = new();

        public static List<Client> ClientList
        {
            get { return clientList; }
        }
    }
}
