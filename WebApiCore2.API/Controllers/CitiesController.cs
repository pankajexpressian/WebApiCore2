using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore2.API.Entities;
using WebApiCore2.API.Models;
using WebApiCore2.API.Services;

namespace WebApiCore2.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public CitiesController(ICityRepository cityRepository,IMapper mapper)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }
        
        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _cityRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities));

        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointOfInterest = false)
        {
            var city = _cityRepository.Get(id, includePointOfInterest);
            if (city==null)
            {
                return NotFound();
            }
            if (includePointOfInterest)
            {
                return Ok(city);
            }
            try
            {

                var cityWithoutPointsOfInterestDto = _mapper.Map<CityWithoutPointsOfInterestDto>(city);

                return Ok(cityWithoutPointsOfInterestDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Mapping Error");


            }

            
        }

        [HttpPost]
        public IActionResult AddCity(CityWithoutPointsOfInterestDto city)
        {
            var cityToAdd = _mapper.Map<City>(city);
            var res = _cityRepository.Add(cityToAdd);
            if (res)
            {
                return StatusCode(201, "Success");
            }
            return StatusCode(500, "Error while saving");
        }
    }
}
