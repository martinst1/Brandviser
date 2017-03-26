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
    public class PendingLogoApprovalViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_PendingLogoApprovalViewModel_WithoutParams()
        {
            // Act & Assert
            var pendingLogoApprovalDomainViewModel = new PendingLogoApprovalDomainViewModel();

            Assert.IsInstanceOf<PendingLogoApprovalDomainViewModel>(pendingLogoApprovalDomainViewModel);
        }

        [Test]
        public void PendingLogoApprovalDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var logoUrl = "logoUrl";
            var pendingLogoApprovalDomainViewModel = new PendingLogoApprovalDomainViewModel()
            {
                LogoUrl = logoUrl
            };

            // Act & Assert
            Assert.AreEqual(logoUrl, pendingLogoApprovalDomainViewModel.LogoUrl);
        }
    }
}
