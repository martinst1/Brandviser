using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Common.Contracts;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Brandviser.Services;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class TransferAmountFromBuyerToSeller_Should
    {
        [Test]
        public void Call_GetByStringId_Of_UserRepository_Twice()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var buyerId = "buyerId";
            var sellerId = "sellerId";

            var buyer = new User() { Id = buyerId };
            var seller = new User() { Id = sellerId };
            var amount = 1m;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(buyerId))
                .Returns(buyer);

            userRepository.Setup(r => r.GetByStringId(sellerId))
               .Returns(seller);

            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);


            // Act
            userService.TransferAmountFromBuyerToSeller(buyerId, amount, sellerId);

            // Assert
            userRepository.Verify(u => u.GetByStringId(buyerId), Times.Once());
            userRepository.Verify(u => u.GetByStringId(sellerId), Times.Once());
        }

        [Test]
        public void Set_Data_Correctly()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var buyerId = "buyerId";
            var sellerId = "sellerId";

            var buyer = new User() { Id = buyerId };
            var seller = new User() { Id = sellerId };
            var amount = 1m;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(buyerId))
                .Returns(buyer);

            userRepository.Setup(r => r.GetByStringId(sellerId))
               .Returns(seller);

            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);

            
            // Act
            userService.TransferAmountFromBuyerToSeller(buyerId, amount, sellerId);

            // Assert
            Assert.AreEqual(-1, buyer.Balance);
            Assert.AreEqual(1, seller.Balance);
        }

        [Test]
        public void Call_SaveChanges_Of_BrandviserData_Twice()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var buyerId = "buyerId";
            var sellerId = "sellerId";

            var buyer = new User() { Id = buyerId };
            var seller = new User() { Id = sellerId };
            var amount = 1m;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(buyerId))
                .Returns(buyer);

            userRepository.Setup(r => r.GetByStringId(sellerId))
               .Returns(seller);

            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);


            // Act
            userService.TransferAmountFromBuyerToSeller(buyerId, amount, sellerId);

            // Assert
            brandviserData.Verify(b => b.SaveChanges(), Times.Exactly(2));
        }
    }
}
