using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Seller.Controllers;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Seller_Area.SellerControllerTests
{
    [TestFixture]
    public class Domains_Should
    {
        [Test]
        public void RenderPartialView_Domains()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            // Act & Assert
            sellerController.WithCallTo(c => c.Domains()).ShouldRenderPartialView("_Domains");
        }
    }
}
