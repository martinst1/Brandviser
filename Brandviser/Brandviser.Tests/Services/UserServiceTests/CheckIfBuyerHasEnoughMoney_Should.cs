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
    public class CheckIfBuyerHasEnoughMoney_Should
    {
        [Test]
        public void Call_GetByStringId_Of_UserRepository_Once()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userid";
            var neededAmount = 1m;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(It.IsAny<string>()))
                .Returns(new User() { Balance = 2m });
            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);

            // Act
            userService.CheckIfBuyerHasEnoughMoney(userId, neededAmount);

            // Assert
            userRepository.Verify(u => u.GetByStringId(userId), Times.Once());
        }

        [Test]
        public void Returns_Correct_Result()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userid";
            var neededAmount = 1m;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(It.IsAny<string>()))
                .Returns(new User() { Balance = 2m });
            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);

            // Act
            var hasNeededAmount = userService.CheckIfBuyerHasEnoughMoney(userId, neededAmount);

            // Assert
            Assert.IsTrue(hasNeededAmount);

        }
    }
}
