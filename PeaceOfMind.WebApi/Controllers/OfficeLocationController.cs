using Microsoft.AspNet.Identity;
using PeaceOfMind.Models;
using PeaceOfMind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PeaceOfMind.WebApi.Controllers
{
    [Authorize]
    public class OfficeLocationController : ApiController
    {
        private OfficeLocationService CreateOfficeLocationService()
        {
            var Id = Guid.Parse(User.Identity.GetUserId());
            var ingredientService = new OfficeLocationService(Id);
            return ingredientService;
        }
        public IHttpActionResult Post([FromBody]OfficeLocationModel model)
        {
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);            
            var service = CreateOfficeLocationService();
            if (!service.CreateOfficeLocation(model))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Get()
        {
            OfficeLocationService OLService = CreateOfficeLocationService();
            var locations = OLService.GetOfficeLocations();
            return Ok(locations);
        }
        public IHttpActionResult GetById([FromUri]int id)
        {
            OfficeLocationService service = CreateOfficeLocationService();
            var officeLocation = service.GetOfficeLocationById(id);
            return Ok(officeLocation);
        }
        public IHttpActionResult Put([FromUri]int id, [FromBody] OfficeLocationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateOfficeLocationService();
            if (!service.UpdateOfficeLocation(id, model))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Delete([FromUri]int id)
        {
            var service = CreateOfficeLocationService();
            if (!service.RemoveOfficeLocation(id))
                return InternalServerError();
            return Ok();
        }
    }
}
