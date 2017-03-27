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
    public class Propose_Should
    {
        public void RenderProposePartialView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var userService = new Mock<IUserService>();
            var loggedInUser = new Mock<ILoggedInUser>();
            var designerController = new DesignerController(userService.Object, domainService.Object, loggedInUser.Object);


            // Act & Assert
            designerController
               .WithCallTo(s => s.Propose("name"))
               .ShouldRenderPartialView("_Propose")
               .WithModel<SubmitLogoViewModel>(
               p =>
               {
                   Assert.AreEqual("name", p.Name);
               });
        }
    }
}
