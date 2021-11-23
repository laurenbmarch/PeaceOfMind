using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Models
{
    public class OfficeLocationModel
    {        
        [Required]
        public int AddressNumber { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Your Street name has to be at least 3 characters.")]
        public string StreetName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Your Location city has to be at least 3 characters.")]
        public string City { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Your location state has to be at least 3 characters.")]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Your location country has to be at least 3 characters.")]
        public string Country { get; set;  }
    }
}
