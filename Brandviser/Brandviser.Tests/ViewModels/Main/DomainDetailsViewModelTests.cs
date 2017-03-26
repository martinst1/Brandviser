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
    public class DomainDetailsViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_DomainDetailsViewModel_WithoutParams()
        {
            // Act & Assert
            var domainDetailsViewModel = new DomainDetailsViewModel();

            Assert.IsInstanceOf<DomainDetailsViewModel>(domainDetailsViewModel);
        }

        [Test]
        public void DomainDetailsViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            int id = 1;
            string name = "name";
            string description = "description";
            string sellerName = "seller name";
            string designerName = "designer name";
            string sellerId = "sellerId";
            DateTime postedOn = new DateTime(17, 01, 01);
            string logoUrl = "logoUrl";
            decimal? price = 1m;

            var domainDetailsViewModel = new DomainDetailsViewModel()
            {
                Id = id,
                Name = name,
                LogoUrl = logoUrl,
                Price = price,
                Description = description,
                SellerName = sellerName,
                DesignerName = designerName,
                SellerId = sellerId,
                PostedOn = postedOn
            };

            // Act & Assert
            Assert.AreEqual(id, domainDetailsViewModel.Id);
            Assert.AreEqual(name, domainDetailsViewModel.Name);
            Assert.AreEqual(logoUrl, domainDetailsViewModel.LogoUrl);
            Assert.AreEqual(price, domainDetailsViewModel.Price);
            Assert.AreEqual(description, domainDetailsViewModel.Description);
            Assert.AreEqual(sellerName, domainDetailsViewModel.SellerName);
            Assert.AreEqual(designerName, domainDetailsViewModel.DesignerName);
            Assert.AreEqual(sellerId, domainDetailsViewModel.SellerId);
            Assert.AreEqual(postedOn, domainDetailsViewModel.PostedOn);
        }
    }
}
