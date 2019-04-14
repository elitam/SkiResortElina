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
    public class RentalController
    {
        public SkiResortContext rentalContext;

        public RentalController()
        {
            this.rentalContext = new SkiResortContext();
        }
        public RentalController(SkiResortContext context)
        {
            this.rentalContext = context;
        }

        /// <summary>
        /// Gives an item from database by id.
        /// </summary>
        public Item GetItem(int id)
        {
            var item = new Item();
            try
            {
                item = this.rentalContext.Items.First(x => x.Id == id);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Invalid id!", e, Color.Pink);

            }
            return item;
        }

        /// <summary>
        /// Gives all items from database.
        /// </summary>
        /// <returns>a list of all items</returns>
        public List<Item> GetAllItems()
        {
            return rentalContext.Items.ToList();
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        public void Add(Item item)
        {
            this.rentalContext.Items.Add(item);
            this.rentalContext.SaveChanges();
        }

        /// <summary>
        /// Removes an item from the database.
        /// </summary>
        public void RemoveById(int id)
        {
            var item = this.GetItem(id);
            this.rentalContext.Items.Remove(item);
            this.rentalContext.SaveChanges();

        }

        /// <summary>
        /// Sets status of a item to Rented.
        /// </summary>
        public void RentItem(int id)
        {
            var item = this.GetItem(id);
            item.Status = "Rented";
            this.rentalContext.SaveChanges();
        }

        /// <summary>
        /// Sets status of a item to Not Rented.
        /// </summary>
        public void ReturnItem(int id)
        {
            var item = this.GetItem(id);
            item.Status = "Not Rented";
            this.rentalContext.SaveChanges();
        }
    }
}
