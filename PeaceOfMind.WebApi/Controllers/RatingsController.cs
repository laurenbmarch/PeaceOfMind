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
    public class RatingsController : ApiController
    {
        /*private RatingsService CreateRatingService()
        {
            var Id = Guid.Parse(User.Identity.GetUserId());
            var ingredientService = new RatingsService(Id);
            return ingredientService;
        }

        public IHttpActionResult Post([FromBody] RatingsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateRatingService();
            if (!service.CreateRating(model))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Get()
        {
            RatingsService service = CreateRatingService();
            var ratings = service.GetRatings();
            return Ok(ratings);
        }

        public IHttpActionResult GetById([FromUri] int id)
        {
            RatingsService service = CreateRatingService();
            var ratingsById = service.GetRatingsById(id);
            return Ok(ratingsById);
        }

        public IHttpActionResult Put([FromUri] int id, [FromBody] RatingsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateRatingService();
            if (!service.UpdateRatings(id, model))
                return InternalServerError();
            return Ok();

        }
        */
    }
}