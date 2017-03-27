using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Brandviser.Web.Controllers;
using Brandviser.Web.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Brandviser.Tests.Controllers.Main.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var homeController = new HomeController(domainService.Object);

            var domains = new List<Domain>()
            {
                new Domain() { Name = "name", LogoUrl = "logourl", OriginalOwnerCustomPrice = 1, Id = 1 }
            };

            domainService.Setup(d => d.GetLatestEightPublishedDomains()).Returns(domains.AsQueryable<Domain>);

            // Act & Assert

            homeController
                .WithCallTo(h => h.Index())
                .ShouldRenderDefaultView()
                .WithModel<SearchViewModel>(
                s =>
                {
                    Assert.AreEqual(1, s.Domains.Count());
                    Assert.AreEqual("name", s.Domains.First().Name);
                    Assert.AreEqual("logourl", s.Domains.First().LogoUrl);
                    Assert.AreEqual(1, s.Domains.First().Price);
                    Assert.AreEqual(1, s.Domains.First().Id);
                });
        }

        [Test]
        public void Call_GetLatestEightPublishedDomains_Of_DomainService_Once()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var homeController = new HomeController(domainService.Object);

            // Act
            homeController.Index();

            // Assert
            domainService.Verify(d => d.GetLatestEightPublishedDomains(), Times.Once());
        }
    }
}
