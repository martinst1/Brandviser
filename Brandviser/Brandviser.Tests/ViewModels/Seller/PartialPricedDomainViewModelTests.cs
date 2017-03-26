using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Web.Areas.Seller.Models;
using NUnit.Framework;

namespace Brandviser.Tests.ViewModels.Seller
{
    [TestFixture]
    public class PartialPricedDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_PartialPricedDomainViewModel_AndInherits_PartialDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var partialPricedDomainViewModel = new PartialPricedDomainViewModel();

            Assert.IsInstanceOf<PartialDomainViewModel>(partialPricedDomainViewModel);
        }

        [Test]
        public void PartialPricedDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            decimal? price = 1m;
            var name = "name";
            var status = "status";

            var partialPricedDomainViewModel = new PartialPricedDomainViewModel()
            {
                Price = price,
                Name = name,
                Status = status
            };

            // Arrange & Assert
            Assert.AreEqual(price, partialPricedDomainViewModel.Price);
            Assert.AreEqual(name, partialPricedDomainViewModel.Name);
            Assert.AreEqual(status, partialPricedDomainViewModel.Status);
        }
    }
}
