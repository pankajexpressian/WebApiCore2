using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore2.API.Entities;

namespace WebApiCore2.API.Contexts
{
    public class CityInfoDbContext : DbContext
    {
        public CityInfoDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            List<City> cities = new List<City>() {
                new City()
                {
                    Id=1, 
                    Name="Narnaul", 
                    Description="Narnaul"
                },
                new City()
                {
                    Id=2, 
                    Name="Rohtak", Description="Rohtak"

                },
                new City()
                {
                    Id=3, Name="Rewari", Description="Rewari"

                },
              
            };

            modelBuilder.Entity<City>().HasData(cities.ToArray());

            List<PointOfInterest> PointsOfInterest = new List<PointOfInterest>();
            PointsOfInterest.Add(new PointOfInterest { Id = 1, Name = "Subhash Park", Description = "Subhash Park", CityId = 1 });
            PointsOfInterest.Add(new PointOfInterest { Id = 2, Name = "Jal Mehal", Description = "Jal Mehal", CityId = 1 });

            PointsOfInterest.Add(new PointOfInterest { Id = 3, Name = "Tiliyar Lake", Description = "Tiliyar Lake", CityId = 2 });
            PointsOfInterest.Add(new PointOfInterest { Id = 4, Name = "Geeta Complex", Description = "Geeta Complex", CityId = 2 });

            PointsOfInterest.Add(new PointOfInterest { Id = 5, Name = "Model Town", Description = "Model Town", CityId = 3 });
            PointsOfInterest.Add(new PointOfInterest { Id = 6, Name = "Huda Park", Description = "Huda Park", CityId = 3 });
            
            modelBuilder.Entity<PointOfInterest>().HasData(PointsOfInterest.ToArray());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    }
}
