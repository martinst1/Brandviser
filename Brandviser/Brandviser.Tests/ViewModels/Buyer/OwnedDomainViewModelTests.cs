using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Web.Areas.Buyer.Models;
using NUnit.Framework;

namespace Brandviser.Tests.ViewModels.Buyer
{
    [TestFixture]
    public class OwnedDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_OwnedDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var ownedDomainViewModel = new OwnedDomainViewModel();

            Assert.IsInstanceOf<OwnedDomainViewModel>(ownedDomainViewModel);
        }

        [Test]
        public void OwnedDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var name = "name";
            var boughtOn = new DateTime(17, 1, 1);
            decimal? price = 1m;
            var seller = "seller";
            var designer = "designer";

            var ownedDomainViewModel = new OwnedDomainViewModel()
            {
                Name = name,
                BoughtOn = boughtOn,
                Price = price,
                Seller = seller,
                Designer = designer
            };

            // Act & Assert
            Assert.AreEqual(name, ownedDomainViewModel.Name);
            Assert.AreEqual(boughtOn, ownedDomainViewModel.BoughtOn);
            Assert.AreEqual(price, ownedDomainViewModel.Price);
            Assert.AreEqual(seller, ownedDomainViewModel.Seller);
            Assert.AreEqual(designer, ownedDomainViewModel.Designer);
        }
    }
}
