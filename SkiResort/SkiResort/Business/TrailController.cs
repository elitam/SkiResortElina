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
    public class TrailController
    {
        private SkiResortContext trailContext;


        public TrailController()
        {
            this.trailContext = new SkiResortContext();
        }
        public TrailController(SkiResortContext context)
        {
            this.trailContext = context;
        }

        /// <summary>
        /// Gives all trails from database.
        /// </summary>
        /// <returns> List of all hikes</returns>
        public List<Trail> GetAll()
        {
            return trailContext.Trails.ToList();
        }

        /// <summary>
        /// Gives a trail from database by id.
        /// <param name="id">The id chosen by the user.</param>
        /// </summary>
        public Trail Get(int id)
        {
            var trail = new Trail();
            try
            {
                trail = this.trailContext.Trails.First(x => x.Id == id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Invalid id!", e, Color.Pink);

            }
            return trail;
        }

        /// <summary>
        /// Adds a trail in database.
        /// <param name="trail">The trail chosen by the user.</param>
        /// </summary>
        public void Add(Trail trail)
        {

            this.trailContext.Trails.Add(trail);
            this.trailContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a trail from the database.
        /// </summary>
        public void Delete(int id)
        {
            var trail = this.Get(id);
            this.trailContext.Trails.Remove(trail);
            this.trailContext.SaveChanges();

        }




    }
}