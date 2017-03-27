using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Admin.Controllers;
using Brandviser.Web.Areas.Admin.Models;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Admin_Area.AdminControllerTests
{
    [TestFixture]
    public class PendingApproval_Should
    {
        [Test]
        public void RenderPartialView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var adminController = new AdminController(userService.Object, domainService.Object, loggedInUser.Object);

          

            var domains = new List<Domain>()
            {
                new Domain() { Name = "name" }
            };

            domainService.Setup(d => d.GetAllDomainsPendingApproval()).Returns(domains);

            // Act & Assert
            adminController
                .WithCallTo(a => a.PendingApproval())
                .ShouldRenderPartialView("_PendingApproval")
                .WithModel<IEnumerable<PendingApprovalDomainViewModel>>(
                p =>
                {
                    Assert.AreEqual(1, p.Count());
                    Assert.AreEqual("name", p.First().Name);
                    Assert.AreEqual("Pending Approval", p.First().Status);
                });
        }
    }
}
