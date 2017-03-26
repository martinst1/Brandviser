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
    public class EditDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_EditDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var editDomainViewModel = new EditDomainViewModel();

            Assert.IsInstanceOf<EditDomainViewModel>(editDomainViewModel);
        }

        [Test]
        public void EditDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var id = 1;
            var name = "name";
            var description = "description12345";
            decimal? price = 1m;
            decimal? ownerCustomPrice = 1m;

            var editDomainViewModel = new EditDomainViewModel()
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                OwnerCustomPrice = ownerCustomPrice
            };

            // Act & Assert
            Assert.AreEqual(id, editDomainViewModel.Id);
            Assert.AreEqual(name, editDomainViewModel.Name);
            Assert.AreEqual(description, editDomainViewModel.Description);
            Assert.AreEqual(price, editDomainViewModel.Price);
            Assert.AreEqual(ownerCustomPrice, editDomainViewModel.OwnerCustomPrice);
        }
    }
}
