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
    public class LiftController
    {
        public SkiResortContext liftContext;


        public LiftController()
        {
            this.liftContext = new SkiResortContext();
        }

        public LiftController(SkiResortContext context)
        {
            this.liftContext = context;
        }

        /// <summary>
        /// Gives all lifts from database.
        /// </summary>
        /// <returns>
        /// a list of all hikes
        /// </returns>
        public List<Lift> GetAll()
        {
            return liftContext.Lifts.ToList();
        }

        /// <summary>
        /// Gives a lift from database by id.
        /// </summary>
        public Lift Get(int id)
        {
            var lift = new Lift();
            try
            {
                lift = this.liftContext.Lifts.First(x => x.Id == id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Invalid id!", e, Color.Pink);

            }
            return lift;
        }

        /// <summary>
        /// Adds a lift.
        /// </summary>
        public void Add(Lift lift)
        {

            this.liftContext.Lifts.Add(lift);
            this.liftContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a lift from the database.
        /// </summary>
        public void Delete(int id)
        {
            var lift = this.Get(id);
            this.liftContext.Lifts.Remove(lift);
            this.liftContext.SaveChanges();

        }
    }
}