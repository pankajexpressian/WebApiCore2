using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCore2.API.Entities;

namespace WebApiCore2.API.Services
{
    public interface ICityRepository
    {
        IEnumerable<City> GetAll();
        City Get(int cityId,bool includePointOfInterest=false);
        bool Add(City city);
    }
}
