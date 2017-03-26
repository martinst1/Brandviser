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
    public class SellerProfileBoxStatsViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_SellerProfileBoxStatsViewModel_WithoutParams()
        {
            // Act & Assert
            var sellerProfileBoxStatsViewModel = new SellerProfileBoxStatsViewModel();

            Assert.IsInstanceOf<SellerProfileBoxStatsViewModel>(sellerProfileBoxStatsViewModel);
        }

        [Test]
        public void SellerProfileBoxStatsViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var fullName = "fullname";
            var initials = "initials";
            var memberSince = new DateTime(17, 1, 1);
            var balanceKUsd = "1k";
            var balance = 1m;
            var submitted = 1;
            var rejected = 1;
            var pending = 1;
            var published = 1;
            var sold = 1;

            var sellerProfileBoxStatsViewModel = new SellerProfileBoxStatsViewModel()
            {
                FullName = fullName,
                Initials = initials,
                MemberSince = memberSince,
                BalanceInKUsd = balanceKUsd,
                Balance = balance,
                PendingDomains = pending,
                SubmittedDomains = submitted,
                RejectedDomains = rejected,
                PublishedDomains = published,
                SoldDomains = sold
            };

            // Act & Assert
            Assert.AreEqual(fullName, sellerProfileBoxStatsViewModel.FullName);
            Assert.AreEqual(initials, sellerProfileBoxStatsViewModel.Initials);
            Assert.AreEqual(memberSince, sellerProfileBoxStatsViewModel.MemberSince);
            Assert.AreEqual(balanceKUsd, sellerProfileBoxStatsViewModel.BalanceInKUsd);
            Assert.AreEqual(balance, sellerProfileBoxStatsViewModel.Balance);
            Assert.AreEqual(pending, sellerProfileBoxStatsViewModel.PendingDomains);
            Assert.AreEqual(submitted, sellerProfileBoxStatsViewModel.SubmittedDomains);
            Assert.AreEqual(rejected, sellerProfileBoxStatsViewModel.RejectedDomains);
            Assert.AreEqual(published, sellerProfileBoxStatsViewModel.PublishedDomains);
            Assert.AreEqual(sold, sellerProfileBoxStatsViewModel.SoldDomains);
        }
    }
}
