using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Act and Assert
            Assert.That(() =>
            new UserService(null),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IBrandviserData"));
        }

        [Test]
        public void Create_Instance_Of_IUserService_WhenArgumentsAreCorrect()
        {
            // Arrange
            var bradviserData = new Mock<IBrandviserData>();


            // Act
            var userService = new UserService(bradviserData.Object);

            // Assert
            Assert.IsInstanceOf<IUserService>(userService);
        }

        [Test]
        public void NotThrow_WhenEverythingIsPassed()
        {
            // Arrange
            var bradviserData = new Mock<IBrandviserData>();

            // Act and Assert
            Assert.DoesNotThrow(() => new UserService(bradviserData.Object));
        }
    }
}
