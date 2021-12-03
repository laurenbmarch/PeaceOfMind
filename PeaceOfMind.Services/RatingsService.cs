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
        public RatingsService(Guid Id)
        {
            _id = Id;
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
        
        public IEnumerable<RatingsGetItem> GetRatings()
        {
            using (var context = new ApplicationDbContext())
            {
                var query =
                    context
                    .Rating
                    .Select(
                    e =>
                        new RatingsGetItem
                        {
                            RatingsId = e.RatingsId,
                            Professionalism = e.Professionalism,
                            Communication = e.Communication,
                            Effectiveness = e.Effectiveness,
                            Avaliability = e.Avaliability
                        }
                    );
                return query.ToArray();

            }
        }

        public RatingsModel GetRatingsById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                            .Rating
                            .Single(e => e.RatingsId == id);
                return
                    new RatingsModel
                    {
                        Professionalism = entity.Professionalism,
                        Communication = entity.Communication,
                        Effectiveness = entity.Effectiveness,
                        Avaliability = entity.Avaliability
                    };
            }
        }

        public bool UpdateRatings(int id, RatingsModel updateModel)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .Rating
                        .Single(e => e.RatingsId == id);
                             entity.Professionalism = updateModel.Professionalism;
                             entity.Communication = updateModel.Communication;
                             entity.Effectiveness = updateModel.Effectiveness;
                             entity.Avaliability = updateModel.Avaliability;
                return context.SaveChanges() == 1;
            }
        }


    }
}

