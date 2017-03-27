using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Services.Contracts;
using Brandviser.Web.Areas.Admin.Controllers;
using Brandviser.Web.Helpers.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Admin_Area.AdminControllerTests
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
            var adminController = new AdminController(userService.Object, domainService.Object, loggedInUser.Object);

            // Act & Assert
            adminController.WithCallTo(a => a.Domains()).ShouldRenderPartialView("_Domains");
        }
    }
}
