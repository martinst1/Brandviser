using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Seller.Controllers;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Seller_Area.SellerControllerTests
{
    [TestFixture]
    public class AddDomain_Should
    {
        [Test]
        public void RenderPartialView_AddDomain()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            // Act & Assert
            sellerController.WithCallTo(c => c.AddDomain()).ShouldRenderPartialView("_AddDomain");
        }
    }
}