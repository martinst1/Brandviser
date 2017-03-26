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
    public class ValidateDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_ValidateDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var validateDomainViewModel = new ValidateDomainViewModel();

            Assert.IsInstanceOf<ValidateDomainViewModel>(validateDomainViewModel);
        }

        [Test]
        public void ValidateDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var id = 1;
            var name = "name";
            var verificationCode = "code";

            var validateDomainViewModel = new ValidateDomainViewModel()
            {
                Id = id,
                Name = name,
                VerificationCode = verificationCode
            };

            // Act & Assert
            Assert.AreEqual(id, validateDomainViewModel.Id);
            Assert.AreEqual(name, validateDomainViewModel.Name);
            Assert.AreEqual(verificationCode, validateDomainViewModel.VerificationCode);
        }
    }
}
