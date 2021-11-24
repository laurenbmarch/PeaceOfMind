using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Data
{
    public class Therapist
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Gender { get; set; }
        [Required]
        public string LicenseOrDegree { get; set; }
        [Required]
        public string AreaOfSpecialty { get; set; }             
        public virtual IEnumerable<OfficeLocations> ListOfOffices { get; set; }
        public Therapist()
        {
            ListOfOffices = new HashSet<OfficeLocations>();
        }
        public virtual Rating Rating { get; set; }        
    }
}
