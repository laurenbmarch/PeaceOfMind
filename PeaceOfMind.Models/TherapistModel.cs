using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeaceOfMind.Data;

namespace PeaceOfMind.Models
{
    public class TherapistModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [Required]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [Required]
        [JsonProperty("Gender")]
        public string Gender { get; set; }
        [Required]
        [JsonProperty("LicenseOrDegree")]
        public string LicenseOrDegree { get; set; }
        [Required]
        [JsonProperty("AreaOfSpecialty")]
        public List<string> AreaOfSpecialty { get; set; }
    }
}
