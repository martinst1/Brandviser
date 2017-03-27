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

namespace Brandviser.Tests.Controllers.Main.DomainControllerTests
{
    [TestFixture]
    public class Search_With_Params_Should
    {
        public void RenderDefaultView_WithExpectedViewModel()
        {
            // Arrange
            var domainService = new Mock<IDomainService>();
            var domainController = new DomainController(domainService.Object);

            var searchedText = "text";

            SearchViewModel searchViewModel = new SearchViewModel() { SearchBoxText = searchedText };

            var domain = new Domain() { Id = 1, Name = "name", LogoUrl = "logourl", OriginalOwnerCustomPrice = 1 };
            domainService.Setup(d => d.Search(searchedText))
                .Returns(new List<Domain>() { domain }.AsQueryable<Domain>());

            // Act & Assert
            domainController
                .WithCallTo(d => d.Search(searchViewModel))
                .ShouldRenderDefaultView()
                .WithModel<SearchViewModel>(
                s =>
                {
                    Assert.AreEqual(10, s.Domains.Count());
                    Assert.AreEqual("name", s.Domains.First().Name);
                    Assert.AreEqual("logourl", s.Domains.First().LogoUrl);
                    Assert.AreEqual(1, s.Domains.First().Price);
                    Assert.AreEqual(1, s.Domains.First().Id);
                });
        }
    }
}
