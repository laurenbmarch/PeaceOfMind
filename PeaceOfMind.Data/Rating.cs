using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Data
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Therapist))]
        public Guid TherapistId { get; set; }
        public virtual Therapist Therapist { get; set; }
        [Required]
        public int Professionalism { get; set; }
        [Required]
        public int Communication { get; set; }
        [Required]
        public int Effectiveness { get; set; }
        [Required]
        public int Avaliability { get; set; }
        [Required]
        public decimal AverageRating { get; set; }

    }
}

