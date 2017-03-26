using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Common.Contracts;
using Brandviser.Data.Contracts;
using Brandviser.Services;
using Brandviser.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_IBrandviserData_WhenBrandviserDataIsNull()
        {
            // Arrange
            var dateTimeProvider = new Mock<IDateTimeProvider>();

            // Act and Assert
            Assert.That(() =>
            new UserService(null, dateTimeProvider.Object),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IBrandviserData"));
        }

        [Test]
        public void Create_Instance_Of_IUserService_WhenArgumentsAreCorrect()
        {
            // Arrange
            var bradviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();


            // Act
            var userService = new UserService(bradviserData.Object, dateTimeProvider.Object);

            // Assert
            Assert.IsInstanceOf<IUserService>(userService);
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var bradviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();


            // Act and Assert
            Assert.DoesNotThrow(() => new UserService(bradviserData.Object, dateTimeProvider.Object));
        }
    }
}
