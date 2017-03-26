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
    public class AddFundsViewModelTests
    {
        [Test]
        public void ConstructorShouldCreate_AddFundsViewModel_WithoutParams()
        {
            // Act & Assert
            var addFundsViewModel = new AddFundsViewModel();

            Assert.IsInstanceOf<AddFundsViewModel>(addFundsViewModel);
        }
        [Test]
        public void AddFundsViewModel_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            decimal amount = 1m;
            var addFundsViewModel = new AddFundsViewModel() { Amount = amount};

            // Act & Assert
            Assert.AreEqual(amount, addFundsViewModel.Amount);
        }
    }
}
