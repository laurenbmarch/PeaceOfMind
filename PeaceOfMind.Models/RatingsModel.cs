using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Models
{
    public class RatingsModel
    {
       [Required]
       public int Professionalism { get; set; }
       [Required]
       public int Communication { get; set; }
       [Required]
       public int Effectiveness { get; set; }
       [Required]
       public int Avaliability { get; set; }

       public decimal AverageRating { get { return Professionalism + Communication + Effectiveness + Avaliability / 4; } }
    }
}
