using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiCore2.API.Models;
using WebApiCore2.API.Services;

namespace WebApiCore2.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/PointsOfInterest")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly CityDataStore _cityDataStore = CityDataStore.Instance;
        private readonly ILogger<PointOfInterestDto> _logger;
        private readonly IMailService _mailService;

        public PointsOfInterestController(ILogger<PointOfInterestDto> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {

            var city = _cityDataStore.Cities.Where(a => a.Id == cityId).FirstOrDefault();
            if (city == null)
            {
                return NotFound();
            }

            var pointsOfInterest = city.PointsOfInterest;
            if (pointsOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointsOfInterest);
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {

            //var city = _cityDataStore.Cities.Where(a => a.Id == cityId).FirstOrDefault();
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointsOfInterest = city.PointsOfInterest.Where(a => a.Id == id).FirstOrDefault();
            //if (pointsOfInterest == null)
            //{
            //    return NotFound();
            //}

            var existingPointOfInterest = GetPointOfInterestByIdAndCityId(id, cityId);
            if (existingPointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(existingPointOfInterest);
        }

        [HttpPost]
        public IActionResult Add(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }


            var city = _cityDataStore.Cities.Where(a => a.Id == cityId).FirstOrDefault();
            if (city == null)
            {
                return BadRequest();
            }
            var maxPointOfInterestId = city.PointsOfInterest.Max(a => a.Id);
            var newPointOfInterest = new PointOfInterestDto()
            {
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
                Id = ++maxPointOfInterestId
            };
            city.PointsOfInterest.Add(newPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new { cityId, id = newPointOfInterest.Id }, newPointOfInterest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, PointOfInterestForUpdateDto pointOfInterest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can't be same");
                return BadRequest(ModelState);
            }
            var existingPointOfInterest = GetPointOfInterestByIdAndCityId(id, cityId);
            if (existingPointOfInterest == null)
            {
                return NotFound();
            }
            existingPointOfInterest.Name = pointOfInterest.Name;
            existingPointOfInterest.Description = pointOfInterest.Description;


            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePartiallyPointOfInterest(int cityId,
            int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> jsonPatchDocument)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPointOfInterest = GetPointOfInterestByIdAndCityId(id, cityId);
            if (existingPointOfInterest == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = existingPointOfInterest.Name,
                Description = existingPointOfInterest.Description
            };

            jsonPatchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Name == pointOfInterestToPatch.Description)
            {
                ModelState.AddModelError("Description", "Name and Description can't be same");
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            existingPointOfInterest.Name = pointOfInterestToPatch.Name;
            existingPointOfInterest.Description = pointOfInterestToPatch.Description;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            try
            {
                var existingPointOfInterest = GetPointOfInterestByIdAndCityId(id, cityId);
                if (existingPointOfInterest == null)
                {
                    return NotFound();
                }

                var city = _cityDataStore.Cities.Where(a => a.Id == cityId).FirstOrDefault();
                city.PointsOfInterest.Remove(city.PointsOfInterest.Where(a => a.Id == id).FirstOrDefault());
                _logger.LogInformation($"Deleted point of interest with id {id} from city with id {cityId}");

                _mailService.SendMail($"Point Of Interest with Id : {id} Deleted", "Content");

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogCritical(ex, $"Error while deleting point of interest with id {id} from city with id {cityId}");
                return StatusCode(500, "Something went wrong at the server, please try again later");
            }
        }

        [NonAction]
        private PointOfInterestDto GetPointOfInterestByIdAndCityId(int id, int cityId)
        {
            var existingCity = _cityDataStore.Cities.Where(a => a.Id == cityId).FirstOrDefault();
            if (existingCity == null)
            {
                return null;
            }
            var existingPointOfInterest = existingCity.PointsOfInterest.Where(a => a.Id == id).FirstOrDefault();
            if (existingPointOfInterest == null)
            {
                return null;
            }
            return existingPointOfInterest;
        }
    }
}