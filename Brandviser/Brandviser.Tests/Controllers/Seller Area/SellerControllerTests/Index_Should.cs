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
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            string userId = "userId";
            var createdOn = new DateTime(17, 1, 1);
            var user = new User() { Id = userId, FirstName = "firstname", LastName = "lastname" };

            var rejectedDomains = new List<Domain>() { new Domain() { StatusId = 2 } };
            var pendingDomains = new List<Domain>() { new Domain() { StatusId = 1 } };
            var publishedDomains = new List<Domain>() { new Domain() { StatusId = 4 } };
            var soldDomains = new List<Domain>() { new Domain() { StatusId = 5 } };

            user.SellerDomains.Add(rejectedDomains[0]);
            user.SellerDomains.Add(pendingDomains[0]);
            user.SellerDomains.Add(publishedDomains[0]);
            user.SellerDomains.Add(soldDomains[0]);
            user.CreatedOn = createdOn;
            user.Balance = 10000;

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            userService.Setup(u => u.GetUserByStringId(userId)).Returns(user);

            // Act & Assert
            sellerController
                .WithCallTo(s => s.Index())
                .ShouldRenderDefaultView()
                .WithModel<SellerProfileBoxStatsViewModel>(
                s =>
                {
                    Assert.AreEqual(user.FirstName + " " + user.LastName, s.FullName);
                    Assert.AreEqual(user.FirstName[0].ToString() + user.LastName[0].ToString(), s.Initials);
                    Assert.AreEqual(createdOn, s.MemberSince);
                    Assert.AreEqual(Math.Round(user.Balance / 1000, 0) + "k", s.BalanceInKUsd);
                    Assert.AreEqual(10000, s.Balance);
                    Assert.AreEqual(4, s.SubmittedDomains);
                    Assert.AreEqual(1, s.RejectedDomains);
                    Assert.AreEqual(1, s.PendingDomains);
                    Assert.AreEqual(1, s.PublishedDomains);
                    Assert.AreEqual(1, s.SoldDomains);
                });
        }

        [Test]
        public void Call_GetUserByStringId_From_UserService_Once()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            string userId = "userId";
            var createdOn = new DateTime(17, 1, 1);
            var user = new User() { Id = userId, FirstName = "firstname", LastName = "lastname" };

            var rejectedDomains = new List<Domain>() { new Domain() { StatusId = 2 } };
            var pendingDomains = new List<Domain>() { new Domain() { StatusId = 1 } };
            var publishedDomains = new List<Domain>() { new Domain() { StatusId = 4 } };
            var soldDomains = new List<Domain>() { new Domain() { StatusId = 5 } };

            user.SellerDomains.Add(rejectedDomains[0]);
            user.SellerDomains.Add(pendingDomains[0]);
            user.SellerDomains.Add(publishedDomains[0]);
            user.SellerDomains.Add(soldDomains[0]);
            user.CreatedOn = createdOn;
            user.Balance = 10000;

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            userService.Setup(u => u.GetUserByStringId(userId)).Returns(user);

            // Act 
            sellerController.Index();

            // Assert
            userService.Verify(u => u.GetUserByStringId(userId), Times.Once());
        }

        [Test]
        public void Call_GetUserId_From_LoggedInUser_Once()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            string userId = "userId";
            var createdOn = new DateTime(17, 1, 1);
            var user = new User() { Id = userId, FirstName = "firstname", LastName = "lastname" };

            var rejectedDomains = new List<Domain>() { new Domain() { StatusId = 2 } };
            var pendingDomains = new List<Domain>() { new Domain() { StatusId = 1 } };
            var publishedDomains = new List<Domain>() { new Domain() { StatusId = 4 } };
            var soldDomains = new List<Domain>() { new Domain() { StatusId = 5 } };

            user.SellerDomains.Add(rejectedDomains[0]);
            user.SellerDomains.Add(pendingDomains[0]);
            user.SellerDomains.Add(publishedDomains[0]);
            user.SellerDomains.Add(soldDomains[0]);
            user.CreatedOn = createdOn;
            user.Balance = 10000;

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            userService.Setup(u => u.GetUserByStringId(userId)).Returns(user);

            // Act
            sellerController.Index();

            // Assert
            loggedInUser.Verify(l => l.GetUserId(), Times.Once());
        }
    }
}
