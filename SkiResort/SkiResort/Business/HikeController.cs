using Microsoft.EntityFrameworkCore;
using SkiResort.Data;
using SkiResort.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace SkiResort.Business
{
    public class HikeController
    {
        private SkiResortContext hikeContext;
        private RateController rateController;


        public HikeController()
        {
            this.hikeContext = new SkiResortContext();
            this.rateController = new RateController();
        }

        public HikeController(SkiResortContext context)
        {
            this.hikeContext = context;
            this.rateController = new RateController();
        }

        /// <summary>
        /// Gives all hikes from database.
        /// </summary>
        /// <returns>a list of all hikes</returns>
        public List<Hike> GetAll()
        {
            return hikeContext.Hikes.ToList();

        }

        /// <summary>
        /// Gives a hikes from database by id.
        /// </summary>
        public Hike Get(int id)
        {
            var hike = new Hike();
            try
            {
                hike = this.hikeContext.Hikes.First(x => x.Id == id);               
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Invalid id!", e, Color.Pink);
                
            }
            return hike;
        }

        /// <summary>
        /// Adds a hike.
        /// </summary>
        public void Add(Hike hike)
        {
            this.hikeContext.Hikes.Add(hike);
            this.hikeContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a hike from the database.
        /// </summary>
        public void Delete(int id)
        {
            var hike = this.Get(id);
            this.hikeContext.Hikes.Remove(hike);
            this.hikeContext.SaveChanges();
        }

        /// <summary>
        /// Calculate the hike rate .
        /// </summary>
        public void CalculateRateHike(int id)
        {
            var hike = this.Get(id);
            rateController.CalculateRateForHike(hike);
        }

        /// <summary>
        /// Updates current values for a hike.
        /// </summary>
        public void Update(Hike hike)
        {
            var currHike = hikeContext.Hikes.Find(hike.Id);
            if (currHike != null)
            {
                hikeContext.Entry(currHike).CurrentValues.SetValues(hike);
                hikeContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Hike does not exist");
            }
        }
    }
}