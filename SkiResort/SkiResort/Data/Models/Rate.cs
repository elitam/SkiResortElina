using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkiResort.Data.Models
{
    public class Rate
    {
        public Rate()
        {

        }
        public Rate(int stars, int hikeId)
        {
            Stars = stars;
            // Hike = hike;
            HikeId = hikeId;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        public int Stars { get; set; }

        public Hike Hike { get; set; }

        public int HikeId { get; set; }


    }
}