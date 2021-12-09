

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
    public class OfficeLocationService
    {
        private readonly Guid _id;
        public OfficeLocationService(Guid Id)
        {
            _id = Id;
        }

        public bool CreateOfficeLocation(OfficeLocationModel model)
        {
            var entity =
                        new OfficeLocation
                        {

                            AddressNumber = model.AddressNumber,
                            StreetName = model.StreetName,
                            City = model.City,
                            State = model.State,
                            ZipCode = model.ZipCode,
                            Country = model.Country
                        };
            using (var context = new ApplicationDbContext())
            {
                context.OfficeLocations.Add(entity);
                return context.SaveChanges() == 1;
            }
        }
        public IEnumerable<OfficeLocationGetItem> GetOfficeLocations()
        {
            using (var context = new ApplicationDbContext())
            {
                var query =
                    context
                        .OfficeLocations
                        .Select(
                        e =>
                            new OfficeLocationGetItem
                            {
                                OfficeLocationId = e.OfficeLocationId,
                               TherapistCount = e.ListOfTherapists.Count,
                                AddressNumber = e.AddressNumber,
                                StreetName = e.StreetName,
                                City = e.City,
                                State = e.State,
                                ZipCode = e.ZipCode,
                                Country = e.Country                                
                            }
                        );
                return query.ToList();
            }
        }
        public OfficeLocationModel GetOfficeLocationById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                                .OfficeLocations
                                .Single(e => e.OfficeLocationId == id);
                return
                    new OfficeLocationModel
                    {
                        AddressNumber = entity.AddressNumber,
                        StreetName = entity.StreetName,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Country = entity.Country,
                        Therapists = ConvertFromTherapistToTherpistModel(entity.ListOfTherapists.ToList())
                    };
            }
        }
        public bool UpdateOfficeLocation(int id, OfficeLocationModel updateModel)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                        .OfficeLocations
                        .Single(e => e.OfficeLocationId == id);
                entity.AddressNumber = updateModel.AddressNumber;
                entity.StreetName = updateModel.StreetName;
                entity.City = updateModel.City;
                entity.State = updateModel.State;
                entity.ZipCode = updateModel.ZipCode;
                entity.Country = updateModel.Country;
                return context.SaveChanges() == 1;
            }
        }        
        public bool RemoveOfficeLocation(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .OfficeLocations
                        .Single(e => e.OfficeLocationId == id);                 

                ctx.OfficeLocations.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool AddTherapistToOffice(int officeId, int therapistId)
        {            
            using (var ctx = new ApplicationDbContext())
            {
                var foundOffice = ctx.OfficeLocations.Single(ol => ol.OfficeLocationId == officeId);
                var foundTherapist = ctx.Therapist.Single(t => t.TherapistId == therapistId);
                foundOffice.ListOfTherapists.Add(foundTherapist);
                var num = ctx.SaveChanges();
                return num == 1;
            }
        }
        private List<TherapistModel> ConvertFromTherapistToTherpistModel(List<Therapist> therapists)        
        {
            List<TherapistModel> therapistModelList = new List<TherapistModel>();
            foreach(Therapist t in therapists)
            {
                TherapistModel therapistModel =
                    new TherapistModel
                    {
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        LicenseOrDegree = t.LicenseOrDegree,
                        AreaOfSpecialty = t.AreaOfSpecialty,
                        Gender = t.Gender
                    };
                therapistModelList.Add(therapistModel);
            }
            return therapistModelList;
        }
    }
}
