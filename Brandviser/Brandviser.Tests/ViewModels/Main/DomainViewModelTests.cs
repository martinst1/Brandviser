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
    public class DomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_DomainViewModel_WithoutParams()
        {
            // Act & Assert
            var domainViewModel = new DomainViewModel();

            Assert.IsInstanceOf<DomainViewModel>(domainViewModel);
        }

        [Test]
        public void DomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            int id = 1;
            string name = "name";
            string logoUrl = "logoUrl";
            decimal? price = 1m;

            var domainViewModel = new DomainDetailsViewModel()
            {
                Id = id,
                Name = name,
                LogoUrl = logoUrl,
                Price = price
            };

            // Act & Assert
            Assert.AreEqual(id, domainViewModel.Id);
            Assert.AreEqual(name, domainViewModel.Name);
            Assert.AreEqual(logoUrl, domainViewModel.LogoUrl);
            Assert.AreEqual(price, domainViewModel.Price);
        }
    }
}
