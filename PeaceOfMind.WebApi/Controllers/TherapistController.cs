using Microsoft.AspNet.Identity;
using PeaceOfMind.Models;
using PeaceOfMind.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PeaceOfMind.WebApi.Controllers
{
    [Authorize]
    public class TherapistController : ApiController
    {
        private TherapistService CreateTherapistService()
        {
            var Id = Guid.Parse(User.Identity.GetUserId());
            var therapistService = new TherapistService(Id);
            return therapistService;
        }
        public IHttpActionResult Post([FromBody]TherapistModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateTherapistService();
            if (!service.CreateTherapist(model))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Get()
        {
            TherapistService therapistService = CreateTherapistService();
            var therapist = therapistService.GetTherapist();
            return Ok(therapist);
        }
        public IHttpActionResult GetById([FromUri]int id)
        {
            TherapistService therapistService = CreateTherapistService();
            var therapist = therapistService.GetTherapistById(id);
            return Ok(therapist);
        }
        public IHttpActionResult Put([FromUri]int id, [FromBody] TherapistModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var therapistService = CreateTherapistService();
            if (!therapistService.UpdateTherapist(id, model))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Delete([FromUri]int id)
        {
            var therapistService = CreateTherapistService();
            if (!therapistService.RemoveTherapist(id))
                return InternalServerError();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult AddRatingToTherapist([FromUri]int therapistId, [FromBody] RatingsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var therapistService = CreateTherapistService();
            if (!therapistService.AddRatingToTherapist(therapistId, model))
                return InternalServerError();
            return Ok();
        }
    }
}