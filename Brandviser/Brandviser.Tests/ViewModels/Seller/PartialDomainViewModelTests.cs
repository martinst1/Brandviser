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
    public class PartialDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_PartialDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var partialDomainViewModel = new PartialDomainViewModel();

            Assert.IsInstanceOf<PartialDomainViewModel>(partialDomainViewModel);
        }

        [Test]
        public void PartialDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var name = "name";
            var status = "status";

            var partialDomainViewModel = new PartialDomainViewModel() { Name = name, Status = status };

            // Act & Assert
            Assert.AreEqual(name, partialDomainViewModel.Name);
            Assert.AreEqual(status, partialDomainViewModel.Status);
        }
    }
}
