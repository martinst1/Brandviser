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
    public class ApproveDomainLogoViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_ApproveDomainLogoViewModel_WithoutParams()
        {
            // Act & Assert
            var approveDomainLogoViewModel = new ApproveDomainLogoViewModel();

            Assert.IsInstanceOf<ApproveDomainLogoViewModel>(approveDomainLogoViewModel);
        }

        [Test]
        public void ApproveDomainLogoViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var logoUrl = "logoUrl";
            var approveDomainLogoViewModel = new ApproveDomainLogoViewModel() { LogoUrl = logoUrl };

            // Act & Assert
            Assert.AreEqual(logoUrl, approveDomainLogoViewModel.LogoUrl);
        }
    }
}