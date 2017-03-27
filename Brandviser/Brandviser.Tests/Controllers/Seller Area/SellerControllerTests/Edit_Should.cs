using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Seller.Controllers;
using Brandviser.Web.Areas.Seller.Models;
using Brandviser.Web.Controllers;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Seller_Area.SellerControllerTests
{
    [TestFixture]
    public class Edit_Should
    {
        [Test]
        public void RedirectToAction_When_Param_IsNull()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            string name = null;

            // Act & Assert
            sellerController
                .WithCallTo(s => s.Edit(name))
                .ShouldRedirectTo(s => s.Index());
        }

        [Test]
        public void RedirectToAction_When_ReturnedDomain_IsNull()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            Domain domain = null;
            string name = "name";

            domainService.Setup(d => d.GetDomainByName(name)).Returns(domain);

            // Act & Assert
            sellerController
                .WithCallTo(s => s.Edit(name))
                .ShouldRedirectTo(s => s.Index());
        }

        [Test]
        public void RenderDefaultView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            string name = "name";

            Guid guid = Guid.NewGuid();

            var domain = new Domain()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Price = 1,
                OriginalOwnerCustomPrice = 1
            };

            domainService.Setup(d => d.GetDomainByName(name + ".com")).Returns(domain);

            // Act & Assert
            sellerController
                .WithCallTo(s => s.Edit(name))
                .ShouldRenderDefaultPartialView()
                .WithModel<EditDomainViewModel>(
                e =>
                {
                    Assert.AreEqual(1, e.Id);
                    Assert.AreEqual("name", e.Name);
                    Assert.AreEqual("description", e.Description);
                    Assert.AreEqual(1, e.Price);
                    Assert.AreEqual(1, e.OwnerCustomPrice);
                });
        }
    }
}
