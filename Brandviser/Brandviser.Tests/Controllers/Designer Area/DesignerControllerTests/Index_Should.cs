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
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var designerController = new DesignerController(userService.Object, domainService.Object, loggedInUser.Object);

            string userId = "userId";
            var createdOn = new DateTime(17, 1, 1);
            var user = new User() { Id = userId, FirstName = "firstname", LastName = "lastname" };

            var pendingDomains = new List<Domain>() { new Domain() { } };
            var publisheDomains = new List<Domain>() { new Domain() { } };
            var allDomainsPendingDesign = new List<Domain>() { new Domain() { } };

            user.CreatedOn = createdOn;
            user.Balance = 10000;

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            userService.Setup(u => u.GetUserByStringId(userId)).Returns(user);

            domainService.Setup(d => d.GetPendingApprovalDomainsSubmittedByDesigner(userId)).Returns(pendingDomains);
            domainService.Setup(d => d.GetPublishedDomainsSubmittedByDesigner(userId)).Returns(publisheDomains);
            domainService.Setup(d => d.GetAllDomainsPendingDesign()).Returns(allDomainsPendingDesign);


            // Act & Assert
            designerController
                .WithCallTo(d => d.Index())
                .ShouldRenderDefaultView()
                .WithModel<DesignerProfileBoxStatsViewModel>(
                s =>
                {
                    Assert.AreEqual(user.FirstName + " " + user.LastName, s.FullName);
                    Assert.AreEqual(user.FirstName[0].ToString() + user.LastName[0].ToString(), s.Initials);
                    Assert.AreEqual(createdOn, s.MemberSince);
                    Assert.AreEqual(Math.Round(user.Balance / 1000, 0) + "k", s.BalanceInKUsd);
                    Assert.AreEqual(10000, s.Balance);
                    Assert.AreEqual(1, s.DomainsPendingLogoDesign);
                    Assert.AreEqual(1, s.Published);
                    Assert.AreEqual(1, s.Submitted);
                });
        }
    }
}
