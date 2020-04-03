using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiCore2.API.Contexts;
using WebApiCore2.API.Entities;

namespace WebApiCore2.API.Services
{
    public class CityRepository : ICityRepository
    {
        private readonly CityInfoDbContext _contex;
       

        public CityRepository(CityInfoDbContext context)
        {
            _contex = context;
        }
        public City Get(int cityId, bool includePointOfInterest = false)
        {
            if (includePointOfInterest)
            {
                return _contex.Cities.Where(a => a.Id == cityId).Include(a => a.PointsOfInterest).FirstOrDefault();
            }
            return _contex.Cities.Where(a => a.Id == cityId).FirstOrDefault();
        }

        public IEnumerable<City> GetAll()
        {
            var cities = _contex.Cities.ToList();
           
            return cities;
        }
    }
}
