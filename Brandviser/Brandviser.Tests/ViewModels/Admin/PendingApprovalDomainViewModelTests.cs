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
    public class PendingApprovalDomainViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_PendingApprovalDomainViewModel_WithoutParams()
        {
            // Act & Assert
            var pendingApprovalDomainViewModel = new PendingApprovalDomainViewModel();

            Assert.IsInstanceOf<PendingApprovalDomainViewModel>(pendingApprovalDomainViewModel);
        }

        [Test]
        public void PendingApprovalDomainViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var name = "name";
            var status = "status";
            var pendingApprovalDomainViewModel = new PendingApprovalDomainViewModel()
            {
                Name = name,
                Status = status
            };

            // Act & Assert
            Assert.AreEqual(name, pendingApprovalDomainViewModel.Name);
            Assert.AreEqual(status, pendingApprovalDomainViewModel.Status);
        }
    }
}
