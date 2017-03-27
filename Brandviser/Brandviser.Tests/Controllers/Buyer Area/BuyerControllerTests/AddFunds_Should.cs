using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Buyer.Controllers;
using Brandviser.Web.Areas.Buyer.Models;
using Brandviser.Web.Areas.Seller.Controllers;
using Brandviser.Web.Areas.Seller.Models;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Buyer_Area.BuyerControllerTests
{
    [TestFixture]
    public class AddFunds_Should
    {
        [Test]
        public void RenderPartialView_AddFunds()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var buyerController = new BuyerController(loggedInUser.Object, domainService.Object, userService.Object);

            // Act & Assert
            buyerController.WithCallTo(c => c.AddFunds()).ShouldRenderPartialView("_AddFunds");
        }
    }
}
