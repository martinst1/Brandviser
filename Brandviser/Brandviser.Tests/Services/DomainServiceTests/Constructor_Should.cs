using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Common.Contracts;
using Brandviser.Data.Contracts;
using Brandviser.Factories;
using Brandviser.Services;
using Brandviser.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Services.DomainServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_IBrandviserData_WhenBrandviserDataIsNull()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act and Assert
            Assert.That(() =>
            new DomainService(null, domainFactory.Object, dateTimeProvider.Object),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IBrandviserData"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_IDomainFactory_WhenDomainFactoryIsNull()
        {
            // Arrange
            var bradviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act and Assert
            Assert.That(() =>
            new DomainService(bradviserData.Object, null, dateTimeProvider.Object),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IDomainFactory"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_IDateTimeProvider_WhenDateTimeProviderIsNull()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var bradviserData = new Mock<IBrandviserData>();

            // Act and Assert
            Assert.That(() =>
            new DomainService(bradviserData.Object, domainFactory.Object, null),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IDateTimeProvider"));
        }

        [Test]
        public void Create_Instance_Of_IDomainService_WhenArgumentsAreCorrect()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var bradviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();


            // Act
            var domainService = new DomainService(bradviserData.Object, domainFactory.Object, dateTimeProvider.Object);

            // Assert
            Assert.IsInstanceOf<IDomainService>(domainService);
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var bradviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act and Assert
            Assert.DoesNotThrow(() => new DomainService(bradviserData.Object, domainFactory.Object, dateTimeProvider.Object));
        }
    }
}
