using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SkiResort.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkiResort.Business;
using SkiResort.Data;

namespace SkiResort.Tests
{
    public class RateControllerTests
    {
        [TestCase]
        public void AddRateToDatabase()
        {
            var data = new List<Rate>()
            {
                new Rate(4,7),
                new Rate(5,7)
            }.AsQueryable();


            var mockSet = new Mock<DbSet<Rate>>();
            mockSet.As<IQueryable<Rate>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Rates).Returns(mockSet.Object);

            RateController service = new RateController(mockContext.Object);
            data.ToList().ForEach(t => service.AddRate(t));
        }

        [TestCase]
        public void GetAllTrailsFromDatabase()
        {
            var data = new List<Rate>
            {
                new Rate { HikeId= 8 },
                new Rate { HikeId=5 },
                new Rate { HikeId=7 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Rate>>();
            mockSet.As<IQueryable<Rate>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Rates).Returns(mockSet.Object);

            var controller = new RateController(mockContext.Object);
            var rates = controller.GetAll();

            Assert.AreEqual(3, rates.Count);
            Assert.AreEqual(8, rates[0].HikeId);
            Assert.AreEqual(5, rates[1].HikeId);
            Assert.AreEqual(7, rates[2].HikeId);
        }

        [TestCase]
        public void GetRateFromDatabaseById()
        {
            var data = new List<Rate>
            {
                new Rate { Id=1,HikeId= 8},
                new Rate { Id=2,HikeId=5  },
                new Rate { Id=3,HikeId=7 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Rate>>();
            mockSet.As<IQueryable<Rate>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Rates).Returns(mockSet.Object);

            var controller = new RateController(mockContext.Object);
            Rate rate = controller.Get(1);


            Assert.AreEqual(8, rate.HikeId);


        }

        [TestCase]
        public void CalculateRateForAHike()
        {
            var data = new List<Rate>
            {
                new Rate {HikeId=8,Stars = 5 },
                new Rate {HikeId=8,Stars = 2 },
                new Rate {HikeId=8,Stars = 5 },
                new Rate {HikeId=9,Stars = 2 }
            }.AsQueryable();

            var expected = 4;

            var mockSet = new Mock<DbSet<Rate>>();
            mockSet.As<IQueryable<Rate>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Rate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.Rates).Returns(mockSet.Object);

            var controller = new RateController(mockContext.Object);
            var hike = new Hike();
            hike.Id = data.First().HikeId;
            var rateStars = controller.CalculateRateForHike(hike);


            Assert.AreEqual(expected, rateStars);



        }
    }
}