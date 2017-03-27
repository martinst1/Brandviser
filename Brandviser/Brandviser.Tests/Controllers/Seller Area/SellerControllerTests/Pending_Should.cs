using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Seller.Controllers;
using Brandviser.Web.Areas.Seller.Models;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Seller_Area.SellerControllerTests
{
    [TestFixture]
    public class Pending_Should
    {
        [Test]
        public void Call_LoggedInUser_GetUserId_Once()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            var userId = "userId";

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            var domains = new List<Domain>()
            {
                new Domain() { Name = "name" }
            };

            domainService.Setup(d => d.GetSellerPendingDomainsByUserId(userId)).Returns(domains.AsQueryable<Domain>());

            // Act
            sellerController.Pending();

            // Assert
            loggedInUser.Verify(l => l.GetUserId(), Times.Once());
        }

        [Test]
        public void RenderDefaultView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            var userId = "userId";

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            var domains = new List<Domain>()
            {
                new Domain() { Name = "name" }
            };

            domainService.Setup(d => d.GetSellerPendingDomainsByUserId(userId)).Returns(domains.AsQueryable<Domain>());

            // Act & Assert
            sellerController
                .WithCallTo(s => s.Pending())
                .ShouldRenderPartialView("_Pending")
                .WithModel<IEnumerable<PartialDomainViewModel>>(
                p =>
                {
                    Assert.AreEqual(1, p.Count());
                    Assert.AreEqual("name", p.First().Name);
                    Assert.AreEqual("Pending", p.First().Status);
                });
        }
    }
}
