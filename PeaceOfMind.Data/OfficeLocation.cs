using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Data
{
    public class OfficeLocation
    {
        

        [Key]           
        public int OfficeLocationId { get; set; }
        //public Guid  Id { get; set; }
        [JsonIgnore]
        public virtual ICollection<Therapist>  ListOfTherapists {get; set;}
        public OfficeLocation()
        {
            ListOfTherapists = new HashSet<Therapist>();
        }
        [Required]
        public int AddressNumber { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Country { get; set; }       
        
     }
}
