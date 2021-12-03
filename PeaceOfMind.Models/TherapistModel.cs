using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Models
{
    public class TherapistModel
    {
        public int Id { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string LicenseOrDegree { get; set; }
        [Required]
        public string AreaOfSpecialty { get; set; }
        
    }
}
