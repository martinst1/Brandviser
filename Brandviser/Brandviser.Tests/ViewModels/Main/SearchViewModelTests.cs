using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Web.Models;
using NUnit.Framework;

namespace Brandviser.Tests.ViewModels.Main
{
    [TestFixture]
    public class SearchViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_SearchViewModel_WithoutParams()
        {
            // Act & Assert
            var searchViewModel = new SearchViewModel();

            Assert.IsInstanceOf<SearchViewModel>(searchViewModel);
        }

        [Test]
        public void SearchViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            string searchBoxTest = "text";

            IEnumerable<DomainViewModel> domains = new List<DomainViewModel>()
            {
                new DomainViewModel()
            };

            var searchViewModel = new SearchViewModel()
            {
                SearchBoxText = searchBoxTest,
                Domains = domains
            };

            // Act & Assert
            Assert.AreEqual(searchBoxTest, searchViewModel.SearchBoxText);
            Assert.AreEqual(domains, searchViewModel.Domains);
        }
    }
}
