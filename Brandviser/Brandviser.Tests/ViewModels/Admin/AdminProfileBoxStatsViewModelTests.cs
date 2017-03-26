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
    public class AdminProfileBoxStatsViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_AdminProfileBoxStatsViewModel_WithoutParams()
        {
            // Act & Assert
            var adminProfileBoxStatsViewModel = new AdminProfileBoxStatsViewModel();

            Assert.IsInstanceOf<AdminProfileBoxStatsViewModel>(adminProfileBoxStatsViewModel);
        }

        [Test]
        public void AdminProfileBoxStatsViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var fullName = "fullName";
            var initials = "initials";
            var dateTime = new DateTime(17, 01, 01);
            var domainsPendingApproval = 1;
            var domainsPendingLogoApproval = 1;
            var adminProfileBoxStatsViewModel = new AdminProfileBoxStatsViewModel()
            {
                FullName = fullName,
                Initials = initials,
                MemberSince = dateTime,
                DomainsPendingApproval = domainsPendingApproval,
                DomainsPendingLogoApproval = domainsPendingLogoApproval
            };

            // Act & Assert
            Assert.AreEqual(fullName, adminProfileBoxStatsViewModel.FullName);
            Assert.AreEqual(initials, adminProfileBoxStatsViewModel.Initials);
            Assert.AreEqual(dateTime, adminProfileBoxStatsViewModel.MemberSince);
            Assert.AreEqual(domainsPendingApproval, adminProfileBoxStatsViewModel.DomainsPendingApproval);
            Assert.AreEqual(domainsPendingLogoApproval, adminProfileBoxStatsViewModel.DomainsPendingLogoApproval);
        }
    }
}
