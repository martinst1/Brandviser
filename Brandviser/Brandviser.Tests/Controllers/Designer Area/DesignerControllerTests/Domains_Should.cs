using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Designer.Controllers;
using Brandviser.Web.Areas.Designer.Models;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Designer_Area.DesignerControllerTests
{
    [TestFixture]
    public class Domains_Should
    {
        [Test]
        public void RenderDefaultView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var designerController = new DesignerController(userService.Object, domainService.Object, loggedInUser.Object);



            var domains = new List<Domain>()
            {
                new Domain() { Name = "name" }
            };

            domainService.Setup(d => d.GetAllDomainsPendingDesign()).Returns(domains);

            // Act & Assert
            designerController
                .WithCallTo(s => s.Domains())
                .ShouldRenderPartialView("_Pending")
                .WithModel<IEnumerable<PendingDesignDomainViewModel>>(
                p =>
                {
                    Assert.AreEqual(1, p.Count());
                    Assert.AreEqual("name", p.First().Name);
                    Assert.AreEqual("Pending Design", p.First().Status);
                    Assert.AreEqual(true, p.First().HasLogoUrl);
                });
        }
    }
}
