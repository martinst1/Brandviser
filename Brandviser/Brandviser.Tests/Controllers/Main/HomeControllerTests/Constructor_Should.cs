using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Services.Contracts;
using Brandviser.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Controllers.Main.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_WhenCorrectParamsArePassed()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();


            // Act and Assert
            Assert.DoesNotThrow(() => new HomeController(domainService.Object));
        }

        [Test]
        public void Create_Instance_Of_Controller_WhenArgumentsAreCorrect()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();


            // Act
            var homeController = new HomeController(domainService.Object);

            // Assert
            Assert.IsInstanceOf<HomeController>(homeController);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_IDomainService_WhenDomainServiceIsNull()
        {
          
            // Act and Assert
            Assert.That(() =>
            new HomeController(null),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IDomainService"));
        }
    }
}
