using SkiResort.Data;
using SkiResort.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SkiResort.Business
{

    public class LiftPassController
    {
        public const double KidsDiscount = 0.5;
        public const double StudentsAndRetieredDiscount = 0.7;

        public SkiResortContext liftPassContext;

        public LiftPassController()
        {
            this.liftPassContext = new SkiResortContext();
        }
        public LiftPassController(SkiResortContext context)
        {
            this.liftPassContext = context;
        }

        /// <summary>
        /// Gives all lift passes from database.
        /// </summary>
        /// <returns>List of all lift passes</returns>
        public List<LiftPass> GetAll()
        {
            return liftPassContext.liftPasses.ToList();
        }

        /// <summary>
        /// This method calculates the price or every type of lift pass.
        /// </summary>
        /// <param name="liftPass">The lift pass chosen by the user.</param>
        /// <returns>Returns the price of the lift pass by the type of the lift pass.</returns>      
        public double CalculatePrice(LiftPass liftPass) 
        {
            double price = liftPass.Price;

            switch (liftPass.Type)
            {
                case "Kid":
                    price *= KidsDiscount;
                    break;
                case "Student":
                    price *= StudentsAndRetieredDiscount;
                    break;
                case "Retired":
                    price *= StudentsAndRetieredDiscount;
                    break;
                default:
                    break;
            }
            return price;

        }

        /// <summary>
        /// Gives a lift pass from database by id.
        /// </summary>
        public LiftPass Get(int id)
        {
            var liftpass = this.liftPassContext.liftPasses.FirstOrDefault(x => x.Id == id);
            return liftpass;
        }

        /// <summary>
        /// Gives a lift pass from database by the type of the lift pass.
        /// </summary>
        /// <returns>Returns lift pass.</returns>
        public LiftPass GetByType(int type)
        {
            var liftPass = this.liftPassContext.liftPasses.First(l => l.Id == type);
            return liftPass;
        }
       
    }
}
