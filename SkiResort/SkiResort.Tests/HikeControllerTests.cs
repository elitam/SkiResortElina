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
    public class HikeControllerTests
    {
        [TestCase]

        public void AddingHikeToDatabase()
        {
            var mockSet = new Mock<DbSet<Hike>>();
            var hike = new Hike();
            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(m => m.Hikes).Returns(mockSet.Object);

            var controller = new HikeController(mockContext.Object);
            controller.Add(hike);

            mockSet.Verify(m => m.Add(It.IsAny<Hike>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }

        [TestCase]
        public void GetAllHikesFromDatabase()
        {
            var data = new List<Hike>
            {
                new Hike { StartPoint="First" },
                new Hike { StartPoint="Second" },
                new Hike { StartPoint="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Hike>>();
            mockSet.As<IQueryable<Hike>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Hike>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Hike>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Hike>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Hikes).Returns(mockSet.Object);

            var controller = new HikeController(mockContext.Object);
            var hikes = controller.GetAll();

            Assert.AreEqual(3, hikes.Count);
            Assert.AreEqual("First", hikes[0].StartPoint);
            Assert.AreEqual("Second", hikes[1].StartPoint);
            Assert.AreEqual("Third", hikes[2].StartPoint);
        }

        [TestCase]
        public void GetHikeFromDatabaseById()
        {
            var data = new List<Hike>
            {
                new Hike { Id=1,StartPoint="First" },
                new Hike { Id=2,StartPoint="Second" },
                new Hike { Id=3,StartPoint="Third" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Hike>>();
            mockSet.As<IQueryable<Hike>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Hike>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Hike>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Hike>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Hikes).Returns(mockSet.Object);

            var controller = new HikeController(mockContext.Object);
            var hike = controller.Get(1);

            Assert.AreEqual("First", hike.StartPoint);


        }
    }
}