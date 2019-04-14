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
    public class LiftPassControllerTests
    {
        [TestCase]
        public void GetAllLiftPassesFromDatabase()
        {
            var data = new List<LiftPass>
            {
                new LiftPass { Type="Adult" },
                new LiftPass { Type="Kid" },
                new LiftPass { Type="Student" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<LiftPass>>();
            mockSet.As<IQueryable<LiftPass>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<LiftPass>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<LiftPass>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<LiftPass>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.liftPasses).Returns(mockSet.Object);

            var controller = new LiftPassController(mockContext.Object);
            var liftPasses = controller.GetAll();

            Assert.AreEqual(3, liftPasses.Count);
            Assert.AreEqual("Adult", liftPasses[0].Type);
            Assert.AreEqual("Kid", liftPasses[1].Type);
            Assert.AreEqual("Student", liftPasses[2].Type);
        }

        [TestCase]
        public void CalculateValidPriceForStudent()
        {
            var data = new List<LiftPass>
            {
                 new LiftPass {Type = "Student",Duration = 91 },
                 new LiftPass {Type = "Adult",Duration = 91, Price=271 }


            }.AsQueryable();

            var expectedResult = 189.7;
            var mockSet = new Mock<DbSet<LiftPass>>();
            mockSet.As<IQueryable<LiftPass>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<LiftPass>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<LiftPass>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<LiftPass>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SkiResortContext>();
            mockContext.Setup(c => c.liftPasses).Returns(mockSet.Object);

            var controller = new LiftPassController(mockContext.Object);

            var liftPasses = controller.GetAll();
            var liftPassStudent = liftPasses[0];
            var liftPassAdult = liftPasses[1];
            liftPassStudent.Price = liftPassAdult.Price;
            liftPassStudent.Price = controller.CalculatePrice(liftPassStudent);

            Assert.AreEqual(expectedResult, liftPassStudent.Price);
        }

    }
}