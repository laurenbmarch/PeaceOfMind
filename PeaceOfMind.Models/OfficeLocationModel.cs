using Newtonsoft.Json;
using PeaceOfMind.Data;
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
        [JsonProperty("AddressNumber")]
        public int AddressNumber { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Your Street name has to be at least 3 characters.")]
        [JsonProperty("StreetName")]
        public string StreetName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Your Location city has to be at least 3 characters.")]
        [JsonProperty("City")]
        public string City { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Your location state has to be at least 3 characters.")]
        [JsonProperty("State")]
        public string State { get; set; }
        [Required]
        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Your location country has to be at least 3 characters.")]
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Therapists")]
        public List<TherapistModel> Therapists { get; set; }
    }
}
