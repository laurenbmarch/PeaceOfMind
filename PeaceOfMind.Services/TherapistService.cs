using PeaceOfMind.Data;
using PeaceOfMind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceOfMind.Services
{
    public class TherapistService
    {
        private readonly Guid _id;

        public TherapistService(Guid Id)
        {
            _id = Id;
        }
        public TherapistService() { }
        public bool CreateTherapist(TherapistModel model)
        {
            var entity =
                new Therapist()
                {
                    //Id = _id,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    LicenseOrDegree = model.LicenseOrDegree,
                    AreaOfSpecialty = model.AreaOfSpecialty
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Therapist.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TherapistGetItem> GetTherapist()
        {
            using (var context = new ApplicationDbContext())
            {
                var query =
                    context
                        .Therapist
                       // .Where(e => e.Id == _id)
                        .Select(
                        e =>
                            new TherapistGetItem
                            {
                                TherapistId = e.TherapistId,
                                LastName = e.LastName,
                                FirstName = e.FirstName
                            }
                        );
                return query.ToArray();
            }
        }

        public Therapist GetTherapistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Therapist
                        .Single(e => e.TherapistId == id); //&& e.Id == _id);
                return
                    new Therapist
                    {
                        TherapistId = entity.TherapistId,                        
                        LastName = entity.LastName,
                        FirstName = entity.FirstName,
                        Gender = entity.Gender,
                        LicenseOrDegree = entity.LicenseOrDegree,
                        AreaOfSpecialty = entity.AreaOfSpecialty
                    };
            }
        }
        public bool UpdateTherapist(int id, TherapistModel updatedModel)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Therapist
                        .Single(e => e.TherapistId == id);// && e.Id == _id);
                entity.LastName = updatedModel.LastName;
                entity.FirstName = updatedModel.FirstName;
                entity.Gender = updatedModel.Gender;
                entity.LicenseOrDegree = updatedModel.LicenseOrDegree;
                entity.AreaOfSpecialty = updatedModel.AreaOfSpecialty;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool RemoveTherapist(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Therapist
                    .Single(e => e.TherapistId == id);// && e.Id == _id);
                ctx.Therapist.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}
