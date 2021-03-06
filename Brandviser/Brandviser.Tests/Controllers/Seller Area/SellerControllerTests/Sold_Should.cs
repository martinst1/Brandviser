﻿using System;
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
    public class Sold_Should
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

            domainService.Setup(d => d.GetSellerSoldDomainsByUserId(userId)).Returns(domains.AsQueryable<Domain>());

            // Act
            sellerController.Accepted();

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

            var dateTime = new DateTime(17, 1, 1);

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            var domains = new List<Domain>()
            {
                new Domain() { Name = "name", Price = 1, SoldOn = dateTime }
            };

            domainService.Setup(d => d.GetSellerSoldDomainsByUserId(userId)).Returns(domains.AsQueryable<Domain>());

            // Act & Assert
            sellerController
                .WithCallTo(s => s.Sold())
                .ShouldRenderPartialView("_Sold")
                .WithModel<IEnumerable<PartialPricedDatedDomainViewModel>>(
                p =>
                {
                    Assert.AreEqual(1, p.Count());
                    Assert.AreEqual("name", p.First().Name);
                    Assert.AreEqual("Sold", p.First().Status);
                    Assert.AreEqual(1, p.First().Price);
                    Assert.AreEqual(dateTime, p.First().SoldOn);
                });
        }
    }
}
