﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Data
{
    class Therapist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string Gender { get; set; }

        [Required]
        public string LicensureOrDegree { get; set; }

        [Required]
        public string AreaOfSpecialty { get; set; }

        [Required]
        public string OfficeLocation { get; set; }

        [Required]
        public double Rating { get; set; }

    }
}
