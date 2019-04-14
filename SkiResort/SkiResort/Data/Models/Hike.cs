using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkiResort.Data.Models
{
    public class Hike
    {
        public Hike()
        {

        }
        public Hike(string startPoint, string endPoint, decimal averageDuration, DateTime startDate)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.AverageDuration = averageDuration;
            this.StartDate = startDate;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public decimal AverageDuration { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public ICollection<Rate> Rates { get; set; }

    }
}