using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore2.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
