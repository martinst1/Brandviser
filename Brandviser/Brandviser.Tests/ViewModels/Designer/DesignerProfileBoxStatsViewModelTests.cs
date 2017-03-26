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
    public class DesignerProfileBoxStatsViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_DesignerProfileBoxStatsViewModel_WithoutParams()
        {
            // Act & Assert
            var designerProfileBoxStatsViewModel = new DesignerProfileBoxStatsViewModel();

            Assert.IsInstanceOf<DesignerProfileBoxStatsViewModel>(designerProfileBoxStatsViewModel);
        }

        [Test]
        public void DesignerProfileBoxStatsViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var fullName = "fullname";
            var initials = "initials";
            var memberSince = new DateTime(17, 1, 1);
            var balanceInKUsd = "1k";
            var balance = 1m;
            var domainsPendingLogoDesign = 1;
            var submitted = 1;
            var published = 1;

            var designerProfileBoxStatsViewModel = new DesignerProfileBoxStatsViewModel()
            {
                FullName = fullName,
                Initials = initials,
                MemberSince = memberSince,
                BalanceInKUsd = balanceInKUsd,
                Balance = balance,
                DomainsPendingLogoDesign = domainsPendingLogoDesign,
                Submitted = submitted,
                Published = published
            };

            // Act & Assert
            Assert.AreEqual(fullName, designerProfileBoxStatsViewModel.FullName);
            Assert.AreEqual(initials, designerProfileBoxStatsViewModel.Initials);
            Assert.AreEqual(memberSince, designerProfileBoxStatsViewModel.MemberSince);
            Assert.AreEqual(balanceInKUsd, designerProfileBoxStatsViewModel.BalanceInKUsd);
            Assert.AreEqual(balance, designerProfileBoxStatsViewModel.Balance);
            Assert.AreEqual(domainsPendingLogoDesign, designerProfileBoxStatsViewModel.DomainsPendingLogoDesign);
            Assert.AreEqual(submitted, designerProfileBoxStatsViewModel.Submitted);
            Assert.AreEqual(published, designerProfileBoxStatsViewModel.Published);
        }
    }
}
