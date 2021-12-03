using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Data
{
    public class OfficeLocationTherapists
    {
        [ForeignKey(nameof(OfficeLocation))]
        public int OfficeLocationId { get; set; }
        public virtual OfficeLocation OfficeLocation { get; set; }

        [ForeignKey(nameof(Therapist))]
        public int TherapistId { get; set; }
        public virtual Therapist Therapist { get; set; }
    }
}
