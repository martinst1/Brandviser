using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Web.Areas.Buyer.Models;
using NUnit.Framework;

namespace Brandviser.Tests.ViewModels.Buyer
{
    [TestFixture]
    public class BuyerProfileBoxStatsViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_BuyerProfileBoxStatsViewModel_WithoutParams()
        {
            // Act & Assert
            var buyerProfileBoxStatsViewModel = new BuyerProfileBoxStatsViewModel();

            Assert.IsInstanceOf<BuyerProfileBoxStatsViewModel>(buyerProfileBoxStatsViewModel);
        }

        [Test]
        public void BuyerProfileBoxStatsViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var fullName = "fullname";
            var initials = "initials";
            var memberSince = new DateTime(17, 1, 1);
            var balanceInKUsd = "1K";
            var balance = 1m;
            var ownedDomains = 1;

            var buyerProfileBoxStatsViewModel = new BuyerProfileBoxStatsViewModel()
            {
                FullName = fullName,
                Initials = initials,
                MemberSince = memberSince,
                BalanceInKUsd = balanceInKUsd,
                Balance = balance,
                OwnedDomains = ownedDomains
            };

            // Act & Assert
            Assert.AreEqual(fullName, buyerProfileBoxStatsViewModel.FullName);
            Assert.AreEqual(initials, buyerProfileBoxStatsViewModel.Initials);
            Assert.AreEqual(memberSince, buyerProfileBoxStatsViewModel.MemberSince);
            Assert.AreEqual(balanceInKUsd, buyerProfileBoxStatsViewModel.BalanceInKUsd);
            Assert.AreEqual(balance, buyerProfileBoxStatsViewModel.Balance);
            Assert.AreEqual(ownedDomains, buyerProfileBoxStatsViewModel.OwnedDomains);
        }
    }
}
