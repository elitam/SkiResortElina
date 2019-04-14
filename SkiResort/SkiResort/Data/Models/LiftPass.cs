using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkiResort.Data.Models
{
    public class LiftPass
    {

        //price calc method
        public LiftPass()
        {

        }
        public LiftPass(string type, double duration, double price, string description)
        {
            this.Type = type;
            this.Duration = duration;
            this.Price = price;
            this.Description = description;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public double Duration { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
