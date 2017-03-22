using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data;
using Brandviser.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Data.BrandviserDataTests
{
    [TestFixture]
    public class SaveChanges_Should
    {
        [Test]
        public void Call_dbContext_SaveChanges_Once()
        {
            // Arrange
            var dbContext = new Mock<IBrandviserDbContext>();
            var brandviserData = new BrandviserData(dbContext.Object);

            // Act
            brandviserData.SaveChanges();

            // Assert
            dbContext.Verify(d => d.SaveChanges(), Times.Once());
        }
    }
}
