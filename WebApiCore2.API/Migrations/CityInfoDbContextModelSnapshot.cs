﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiCore2.API.Contexts;

namespace WebApiCore2.API.Migrations
{
    [DbContext(typeof(CityInfoDbContext))]
    partial class CityInfoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiCore2.API.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new { Id = 1, Description = "Narnaul", Name = "Narnaul" },
                        new { Id = 2, Description = "Rohtak", Name = "Rohtak" },
                        new { Id = 3, Description = "Rewari", Name = "Rewari" }
                    );
                });

            modelBuilder.Entity("WebApiCore2.API.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointsOfInterest");

                    b.HasData(
                        new { Id = 1, CityId = 1, Description = "Subhash Park", Name = "Subhash Park" },
                        new { Id = 2, CityId = 1, Description = "Jal Mehal", Name = "Jal Mehal" },
                        new { Id = 3, CityId = 2, Description = "Tiliyar Lake", Name = "Tiliyar Lake" },
                        new { Id = 4, CityId = 2, Description = "Geeta Complex", Name = "Geeta Complex" },
                        new { Id = 5, CityId = 3, Description = "Model Town", Name = "Model Town" },
                        new { Id = 6, CityId = 3, Description = "Huda Park", Name = "Huda Park" }
                    );
                });

            modelBuilder.Entity("WebApiCore2.API.Entities.PointOfInterest", b =>
                {
                    b.HasOne("WebApiCore2.API.Entities.City", "City")
                        .WithMany("PointsOfInterest")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
