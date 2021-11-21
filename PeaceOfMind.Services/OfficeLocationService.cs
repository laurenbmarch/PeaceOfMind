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
                        new OfficeLocations
                        {
                            Id = _id,
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
                        .Where(e => e.Id == _id)
                        .Select(
                        e =>
                            new OfficeLocationGetItem
                            {
                                OfficeLocationId = e.OfficeLocationId,
                                AddressNumber = e.AddressNumber,
                                StreetName = e.StreetName,
                                City = e.City,
                                State = e.State,
                                ZipCode = e.ZipCode,
                                Country = e.Country
                            }
                        );
                return query.ToArray();
            }
        }
        public OfficeLocationModel GetOfficeLocationById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity =
                    context
                                .OfficeLocations
                                .Single(e => e.OfficeLocationId == id && e.Id == _id);
                return
                    new OfficeLocationModel
                    {
                        AddressNumber = entity.AddressNumber,
                        StreetName = entity.StreetName,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Country = entity.Country
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
                        .Single(e => e.OfficeLocationId == id && e.Id == _id);
                entity.AddressNumber = updateModel.AddressNumber;
                entity.StreetName = updateModel.StreetName;
                entity.City = updateModel.City;
                entity.State = updateModel.State;
                entity.ZipCode = updateModel.ZipCode;
                entity.Country = updateModel.Country;
                return context.SaveChanges() == 1;
            }
        }
    }
}
