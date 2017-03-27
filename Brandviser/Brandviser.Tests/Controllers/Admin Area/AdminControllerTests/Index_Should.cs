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
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var adminController = new AdminController(userService.Object, domainService.Object, loggedInUser.Object);

            string userId = "userId";
            var createdOn = new DateTime(17, 1, 1);
            var user = new User() { Id = userId, FirstName = "firstname", LastName = "lastname", CreatedOn = createdOn };

            var domains = new List<Domain>() { new Domain() };

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            userService.Setup(u => u.GetUserByStringId(userId)).Returns(user);

            domainService.Setup(d => d.GetAllDomainsPendingApproval()).Returns(domains);
            domainService.Setup(d => d.GetAllDomainsPendingLogoApproval()).Returns(domains);

            // Act & Assert
            adminController
                .WithCallTo(a => a.Index())
                .ShouldRenderDefaultView()
                .WithModel<AdminProfileBoxStatsViewModel>(
                s =>
                {
                    Assert.AreEqual(user.FirstName + " " + user.LastName, s.FullName);
                    Assert.AreEqual(createdOn, s.MemberSince);
                    Assert.AreEqual(1, s.DomainsPendingApproval);
                    Assert.AreEqual(1, s.DomainsPendingLogoApproval);
                });
        }
    }
}
