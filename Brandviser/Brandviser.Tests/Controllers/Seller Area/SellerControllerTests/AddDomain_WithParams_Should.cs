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
    public class AddDomain_WithParams_Should
    {
        [Test]
        public void Call_GetUserId_From_LoggedInUser_Once()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            var userId = "userId";

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            AddDomainViewModel addDomainViewModel = new AddDomainViewModel()
            {
                Name = "name",
                Description = "description"
            };

            // Act
            sellerController.AddDomain(addDomainViewModel);

            // Assert
            loggedInUser.Verify(l => l.GetUserId(), Times.Once());
        }

        [Test]
        public void Return_SameView_When_ModelState_is_Invalid()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);
            sellerController.ModelState.AddModelError("", "dummy error");

            var userId = "userId";

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            AddDomainViewModel addDomainViewModel = new AddDomainViewModel()
            {
                Name = "name",
                Description = "description"
            };

            // Act & Assert
            sellerController
                .WithCallTo(s => s.AddDomain(addDomainViewModel))
                .ShouldRenderDefaultView()
                .WithModel<AddDomainViewModel>();
        }

        [Test]
        public void Call_DomainService_AddDomain_Once()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            var userId = "userId";

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            AddDomainViewModel addDomainViewModel = new AddDomainViewModel()
            {
                Name = "name",
                Description = "description"
            };

            // Act
            sellerController.AddDomain(addDomainViewModel);

            // Assert
            domainService.Verify(
                d => d.AddDomain(addDomainViewModel.Name, addDomainViewModel.Description, userId), Times.Once());
        }

        [Test]
        public void Set_TempData_SuccessMessage()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var sellerController = new SellerController(userService.Object, domainService.Object, loggedInUser.Object);

            var userId = "userId";

            loggedInUser.Setup(l => l.GetUserId()).Returns(userId);

            AddDomainViewModel addDomainViewModel = new AddDomainViewModel()
            {
                Name = "name",
                Description = "description"
            };

            // Act 
            sellerController.AddDomain(addDomainViewModel);

            // Assert
            sellerController.ShouldHaveTempDataProperty("Success", "Added successfully!");
        }
    }
}
