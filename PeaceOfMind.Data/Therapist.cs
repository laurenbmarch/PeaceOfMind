using Newtonsoft.Json;
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
        public int TherapistId { get; set; }
        //public Guid Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Gender { get; set; }
        [Required]
        public string LicenseOrDegree { get; set; }
        [Required]
        public AreaOfSpecialty AreaOfSpecialty { get; set; }             
        [JsonIgnore]
        public virtual ICollection<OfficeLocation> ListOfOffices { get; set; }
        public Therapist()
        {
            ListOfOffices = new HashSet<OfficeLocation>();
        }
        [JsonIgnore]
        public virtual List<Rating> Ratings {get; set;}

    }
    public enum AreaOfSpecialty
    {
        Psychotherapy,
        Anxiety,
        Depression,
        Family,
        Marriage,
        Young_Adult,
        Substance_Abuse,
        Grief,
        Trauma,


    }

}
