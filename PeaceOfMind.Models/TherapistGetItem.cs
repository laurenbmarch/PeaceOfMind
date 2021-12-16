﻿using PeaceOfMind.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Models
{
    public class TherapistGetItem
    {
        public int TherapistId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public List<Rating> Ratings { get; set; }

        public AreaOfSpecialty AreaOfSpecialty { get; set; }
        
    }
}
