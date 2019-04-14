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
    public class LiftControllerTests
    {
        [TestCase]
        public void AddTrailToDatabase()
        {
            var data = new List<Lift>()
            {
                new Lift("a",125, 75,"9am-5pm",true),
                new Lift("a",155, 55,"9am-5pm",true)
            }.AsQueryable();


            var mockSet = new Mock<DbSet<Lift>>();
            mockSet.As<IQueryable<Lift>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Lifts).Returns(mockSet.Object);

            var service = new LiftController(mockContext.Object);
            data.ToList().ForEach(t => service.Add(t));
        }

        [TestCase]
        public void GetAllLiftsFromDatabase()
        {
            var data = new List<Lift>
            {
                new Lift { Name="First" },
                new Lift { Name="Second" },
                new Lift { Name="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lift>>();
            mockSet.As<IQueryable<Lift>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Lifts).Returns(mockSet.Object);

            var controller = new LiftController(mockContext.Object);
            var lifts = controller.GetAll();

            Assert.AreEqual(3, lifts.Count);
            Assert.AreEqual("First", lifts[0].Name);
            Assert.AreEqual("Second", lifts[1].Name);
            Assert.AreEqual("Third", lifts[2].Name);
        }

        [TestCase]
        public void GetLiftFromDatabaseById()
        {
            var data = new List<Lift>
            {
                new Lift { Id=1,Name="First" },
                new Lift { Id=2,Name="Second" },
                new Lift { Id=3,Name="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Lift>>();
            mockSet.As<IQueryable<Lift>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Lift>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Lifts).Returns(mockSet.Object);

            var controller = new LiftController(mockContext.Object);
            var lift = controller.Get(1);

            Assert.AreEqual("First", lift.Name);

        }


    }
}