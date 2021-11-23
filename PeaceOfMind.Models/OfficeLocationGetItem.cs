using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Models
{
    public class OfficeLocationGetItem
    {
       public int OfficeLocationId { get; set; }
        public int AddressNumber { get; set; }    
        public string StreetName { get; set; } 
        public string City { get; set; }
        public string State { get; set; }    
        public string ZipCode { get; set; }   
        public string Country { get; set; }
    }
}
