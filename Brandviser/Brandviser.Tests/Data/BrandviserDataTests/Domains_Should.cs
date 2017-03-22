using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Data.BrandviserDataTests
{
    [TestFixture]
    public class Domains_Should
    {
        [Test]
        public void Return_DomainsRepository()
        {
            // Arrange
            var dbContext = new Mock<IBrandviserDbContext>();
            var brandviserData = new BrandviserData(dbContext.Object);

            // Act & Assert
            Assert.IsInstanceOf<IEfRepository<Domain>>(brandviserData.Domains);
        }

        [Test]
        public void Return_Same_Instance_When_Called_MoreThanOnce()
        {
            // Arrange
            var dbContext = new Mock<IBrandviserDbContext>();
            var brandviserData = new BrandviserData(dbContext.Object);

            // Act
            var expected = brandviserData.Domains;
            var actual = brandviserData.Domains;

            // Assert
            Assert.AreSame(expected, actual);
        }
    }
}
