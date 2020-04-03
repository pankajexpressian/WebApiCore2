using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore2.API.Models;

namespace WebApiCore2.API
{
    public class CityDataStore
    {
        public static CityDataStore Instance { get; } = new CityDataStore();
        public List<CityDto> Cities;
        public CityDataStore()
        {
            Cities = new List<CityDto>() {
                new CityDto()
                {
                    Id=1, Name="Narnaul", Description="Narnaul",
                    PointsOfInterest=new List<PointOfInterestDto>(){
                    new PointOfInterestDto{Id=1,Name="Subhash Park",Description="Subhash Park"},
                      new PointOfInterestDto{Id=2,Name="Jal Mehal",Description="Jal Mehal"}
                    }
                },
                new CityDto()
                {
                    Id=2, Name="Rohtak", Description="Rohtak",
                    PointsOfInterest=new List<PointOfInterestDto>(){
                    new PointOfInterestDto{Id=3,Name="Tiliyar Lake",Description="Tiliyar Lake"},
                    new PointOfInterestDto{Id=4,Name="Geeta Complex",Description="Geeta Complex"}
                    }
                },
                new CityDto()
                {
                    Id=3, Name="Rewari", Description="Rewari",
                    PointsOfInterest=new List<PointOfInterestDto>(){
                    new PointOfInterestDto{Id=5,Name="Model Town",Description="Model Town"},
                    new PointOfInterestDto{Id=6,Name="Huda Park",Description="Huda Park"}
                    }
                }
            };
        }
    }
}
