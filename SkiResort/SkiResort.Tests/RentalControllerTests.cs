using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SkiResort.Business;
using SkiResort.Data;
using SkiResort.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiResort.Tests
{
    public class RentalControllerTests
    {

        [TestCase]
        public void AddItemToDatabase()
        {
            var data = new List<Item>()
            {
                new Item("Bord", 45 , "152","Kid"),
                new Item("Bord1", 45 , "185","Male")
            }.AsQueryable();


            var mockSet = new Mock<DbSet<Item>>();
            mockSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Items).Returns(mockSet.Object);

            var service = new RentalController(mockContext.Object);
            data.ToList().ForEach(t => service.Add(t));
        }

        [TestCase]
        public void GetAllItemsFromDatabase()
        {
            var data = new List<Item>
            {
                new Item { Name="First" },
                new Item { Name="Second" },
                new Item { Name="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Item>>();
            mockSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Items).Returns(mockSet.Object);

            var controller = new RentalController(mockContext.Object);
            var items = controller.GetAllItems();

            Assert.AreEqual(3, items.Count);
            Assert.AreEqual("First", items[0].Name);
            Assert.AreEqual("Second", items[1].Name);
            Assert.AreEqual("Third", items[2].Name);
        }

        [TestCase]
        public void GetItem()
        {
            var data = new List<Item>
            {
                new Item { Id=1,Name="First" },
                new Item { Id=2,Name="Second" },
                new Item { Id=3,Name="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Item>>();
            mockSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Items).Returns(mockSet.Object);

            var controller = new RentalController(mockContext.Object);
            var item = controller.GetItem(1);

            Assert.AreEqual("First", item.Name);


        }

        [TestCase]
        public void RentItemReturnRented()
        {
            var data = new List<Item>
            {
                new Item { Id=1,Name="First" }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Item>>();
            mockSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Items).Returns(mockSet.Object);

            var controller = new RentalController(mockContext.Object);

            var item = controller.GetItem(1);
            controller.RentItem(item.Id);


            Assert.AreEqual("Rented", item.Status);
        }


        [TestCase]
        public void ReturnItemReturnNotRented()
        {
            var data = new List<Item>
            {
                new Item { Id=1,Name="First" }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Item>>();
            mockSet.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Items).Returns(mockSet.Object);

            var controller = new RentalController(mockContext.Object);

            var item = controller.GetItem(1);
            controller.ReturnItem(item.Id);


            Assert.AreEqual("Not Rented", item.Status);
        }


    }
}