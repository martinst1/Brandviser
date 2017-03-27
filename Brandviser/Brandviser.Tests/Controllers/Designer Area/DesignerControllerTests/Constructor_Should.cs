using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Designer.Controllers;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Controllers.Designer_Area.DesignerControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_WhenCorrectParamsArePassed()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();

            // Act and Assert
            Assert.DoesNotThrow(() => new DesignerController(
                 userService.Object, domainService.Object, loggedInUser.Object));
        }

        [Test]
        public void Create_Instance_Of_Controller_WhenArgumentsAreCorrect()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();

            // Act
            var designerController = new DesignerController(userService.Object, domainService.Object, loggedInUser.Object);

            // Assert
            Assert.IsInstanceOf<Controller>(designerController);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_IDomainService_WhenDomainServiceIsNull()
        {
            // Arrange
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();

            // Act and Assert
            Assert.That(() =>
            new DesignerController(userService.Object, null, loggedInUser.Object),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IDomainService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_IUserService_WhenUserServiceIsNull()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var loggedInUser = new Mock<ILoggedInUser>();


            // Act and Assert
            Assert.That(() =>
            new DesignerController(null, domainService.Object, loggedInUser.Object),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IUserService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_ILoggedInUser_WhenLoggedInUserIsNull()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();

            // Act and Assert
            Assert.That(() =>
            new DesignerController(userService.Object, domainService.Object, null),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("ILoggedInUser"));
        }
    }
}
