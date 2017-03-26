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
    public class PartialPricedDatedDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_PartialPricedDatedDomainViewModel_AndInherits_PartialPricedDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var partialPricedDatedDomainViewModel = new PartialPricedDatedDomainViewModel();

            Assert.IsInstanceOf<PartialPricedDomainViewModel>(partialPricedDatedDomainViewModel);
        }

        [Test]
        public void ConstructorShouldCreate_PartialPricedDatedDomainViewModel_AndInherits_PartialDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var partialPricedDatedDomainViewModel = new PartialPricedDatedDomainViewModel();

            Assert.IsInstanceOf<PartialDomainViewModel>(partialPricedDatedDomainViewModel);
        }

        [Test]
        public void PartialPricedDatedDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var name = "name";
            var status = "status";
            var soldOn = new DateTime(17, 1, 1);
            decimal? price = 1m;

            var partialPricedDatedDomainViewModel = new PartialPricedDatedDomainViewModel()
            {
                Name = name,
                Status = status,
                SoldOn = soldOn,
                Price = price
            };

            // Act & Assert
            Assert.AreEqual(name, partialPricedDatedDomainViewModel.Name);
            Assert.AreEqual(status, partialPricedDatedDomainViewModel.Status);
            Assert.AreEqual(soldOn, partialPricedDatedDomainViewModel.SoldOn);
            Assert.AreEqual(price, partialPricedDatedDomainViewModel.Price);
        }
    }
}
