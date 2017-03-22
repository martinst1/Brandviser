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
    public class Constructor_Should
    {
        [Test]
        public void ConstructorShouldCreateBrandviserData_WithValidParams()
        {
            // Arrange
            var dbContext = new Mock<IBrandviserDbContext>();

            // Act & Assert
            var brandviserData = new BrandviserData(dbContext.Object);

            Assert.IsInstanceOf<IBrandviserData>(brandviserData);
        }

        [Test]
        public void Throw_If_IBrandviserDbContext_IsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BrandviserData(null));
        }
    }
}
