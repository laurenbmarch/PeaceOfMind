using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Models
{
    public class RatingsGetItem
    {
        public int RatingsId { get; set; }
        public int Professionalism { get; set; }
        public int Communication { get; set; }
        public int Effectiveness { get; set; }
        public int Avaliability { get; set; }
    }
}
