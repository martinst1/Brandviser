using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Brandviser.Web.Controllers;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Main.DomainControllerTests
{
    [TestFixture]
    public class Details_Should
    {
        [Test]
        [TestCase(null)]
        [TestCase(-1)]
        [TestCase(0)]
        public void RedirectToAction_When_PassedId_Is_Null_Or_Less_Than_1(int? id)
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var domainController = new DomainController(domainService.Object);

            // Act & Assert
            domainController
                .WithCallTo(c => c.Details(id))
                .ShouldRedirectTo<HomeController>(h => h.Index());
        }

        [Test]
        public void Call_GetDomainById_From_DomainService_Once()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var domainController = new DomainController(domainService.Object);
            var testId = 1;
            
            var seller = new User() { Id = "id", FirstName = "firstname", LastName = "lastname" };
            var designer = new User() { FirstName = "firstname", LastName = "lastname" };

            var domain = new Domain()
            {
                Id = testId,
                Name = "name",
                Description = "description",
                Price = 1,
                LogoUrl = "logourl",
                UserId = "id",
                UpdatedAt = new DateTime(17,1,1),
                User = seller,
                Designer = designer
            };

            domainService.Setup(d => d.GetDomainById(testId)).Returns(domain);

            // Act 
            domainController.Details(testId);

            //
            domainService.Verify(d => d.GetDomainById(testId), Times.Once());
        }

        [Test]
        public void RedirectToAction_When_Returned_Domain_IsNull()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var domainController = new DomainController(domainService.Object);
            var testId = 1;

            Domain domain = null;

            domainService.Setup(d => d.GetDomainById(testId)).Returns(domain);

            // Act 
            domainController.Details(testId);

            //
            domainController
                .WithCallTo(c => c.Details(testId))
                .ShouldRedirectTo<HomeController>(h => h.Index());
        }
    }
}
