using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Brandviser.Services.Contracts;
using Brandviser.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Controllers.Main.DomainControllerTests
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
            Assert.DoesNotThrow(() => new DomainController(domainService.Object));
        }

        [Test]
        public void Create_Instance_Of_Controller_WhenArgumentsAreCorrect()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();


            // Act
            var domainController = new DomainController(domainService.Object);

            // Assert
            Assert.IsInstanceOf<Controller>(domainController);
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContaining_IDomainService_WhenDomainServiceIsNull()
        {

            // Act and Assert
            Assert.That(() =>
            new DomainController(null),
            Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("IDomainService"));
        }
    }
}
