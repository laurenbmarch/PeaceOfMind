using PeaceOfMind.Data;
using PeaceOfMind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PeaceOfMind.Services
{
    public class RatingsService
    {
        private readonly Guid _id;
        public RatingsService(Guid id)
        {            
            _id = id;
        }
        public bool CreateRating(RatingsModel model)
        {
            var entity = new Rating
            {
                Professionalism = model.Professionalism,
                Communication = model.Communication,
                Effectiveness = model.Effectiveness,
                Avaliability = model.Avaliability
            };
            using (var context = new ApplicationDbContext())
            {
                context.Rating.Add(entity);
                return context.SaveChanges() == 1;
            }
        }
    }
}

