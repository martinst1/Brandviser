using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Web.Areas.Admin.Models;
using NUnit.Framework;

namespace Brandviser.Tests.ViewModels.Admin
{
    [TestFixture]
    public class ApproveDomainNameViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_ApproveDomainNameViewModel_WithoutParams()
        {
            // Act & Assert
            var approveDomainNameViewModel = new ApproveDomainNameViewModel();

            Assert.IsInstanceOf<ApproveDomainNameViewModel>(approveDomainNameViewModel);
        }

        [Test]
        public void ApproveDomainNameViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var id = 1;
            var name = "name";
            var sellerName = "name";
            var sellerUserName = "username";
            decimal? price = 1m;
            var approveDomainNameViewModel = new ApproveDomainLogoViewModel()
            {
                Id = id,
                Name = name,
                SellerName = sellerName,
                SellerUsername = sellerUserName,
                Price = price
            };

            // Act & Assert
            Assert.AreEqual(id, approveDomainNameViewModel.Id);
            Assert.AreEqual(name, approveDomainNameViewModel.Name);
            Assert.AreEqual(sellerName, approveDomainNameViewModel.SellerName);
            Assert.AreEqual(sellerUserName, approveDomainNameViewModel.SellerUsername);
            Assert.AreEqual(price, approveDomainNameViewModel.Price);
        }
    }
}
