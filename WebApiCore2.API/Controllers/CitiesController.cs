using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore2.API.Models;

namespace WebApiCore2.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CityDataStore.Instance.Cities);


        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city=CityDataStore.Instance.Cities.Where(a => a.Id == id).FirstOrDefault();
            if (city==null)
            {
                return NotFound();
            }
            return Ok(city);

        }

        [HttpPost]
        public JsonResult AddCity([FromBody]CityDto city)
        {
            city.Id = CityDataStore.Instance.Cities.Count() + 1;
            CityDataStore.Instance.Cities.Add(city);
            return new JsonResult(city);
        }
    }
}
