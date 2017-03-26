using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Web.Areas.Designer.Models;
using NUnit.Framework;

namespace Brandviser.Tests.ViewModels.Designer
{
    [TestFixture]
    public class PendingDesignDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_PendingDesignDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var pendingDesignDomainViewModel = new PendingDesignDomainViewModel();

            Assert.IsInstanceOf<PendingDesignDomainViewModel>(pendingDesignDomainViewModel);
        }

        [Test]
        public void PendingDesignDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var name = "name";
            var status = "status";
            var hasLogoUrl = true;

            var pendingDesignDomainViewModel = new PendingDesignDomainViewModel()
            {
                Name = name,
                Status = status,
                HasLogoUrl = hasLogoUrl
            };

            // Act & Assert
            Assert.AreEqual(name, pendingDesignDomainViewModel.Name);
            Assert.AreEqual(status, pendingDesignDomainViewModel.Status);
            Assert.AreEqual(hasLogoUrl, pendingDesignDomainViewModel.HasLogoUrl);
        }
    }
}
