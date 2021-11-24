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

        public bool CreateTherapist(TherapistModel model)
        {
            var entity =
                new Therapist()
                {
                    Id = _id,
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

    }
}
