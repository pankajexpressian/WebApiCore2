using System.Collections.Generic;
using WebApiCore2.API.Entities;

namespace WebApiCore2.API.Models
{
    public class CityForCreationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();
    }
}
