using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Data
{
    public class OfficeLocations
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid TherapistId { get; set; }

        public virtual IEnumerable<Therapist>  Therapists {get; set;}
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
