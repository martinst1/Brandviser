using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Services.Contracts;
using Brandviser.Web.Controllers;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Main.DomainControllerTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void RenderDefaultView()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var domainController = new DomainController(domainService.Object);

            // Act & Assert
            domainController.WithCallTo(c => c.Search()).ShouldRenderDefaultView();
        }
    }
}
