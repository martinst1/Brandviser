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
    public class AddDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_AddDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var addDomainViewModel = new AddDomainViewModel();

            Assert.IsInstanceOf<AddDomainViewModel>(addDomainViewModel);
        }

        [Test]
        public void AddDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var name = "name.com";
            var description = "description123457";

            var addDomainViewModel = new AddDomainViewModel()
            {
                Name = name,
                Description = description
            };

            // Act & Assert
            Assert.AreEqual(name, addDomainViewModel.Name);
            Assert.AreEqual(description, addDomainViewModel.Description);
        }
    }
}
