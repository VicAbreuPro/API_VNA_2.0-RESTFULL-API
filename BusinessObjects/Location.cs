using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_VNA_2._0.BusinessObjects
{
    public class Location
    {
        public string localName { get; set; }
        public int frequency { get; set; }

        public Location() { } //Default Constructor
    }
}
