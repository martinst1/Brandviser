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
    public class TopUserBalance_Should
    {
        [Test]
        public void Call_GetByStringId_Of_UserRepository_Once()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userId";

            var user = new User() { Id = userId };
            var amount = 1m;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(userId))
                .Returns(user);

            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);


            // Act
            userService.TopUpUserBalance(userId, amount);

            // Assert
            userRepository.Verify(u => u.GetByStringId(userId), Times.Once());
        }

        [Test]
        public void Set_Data_Correctly()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userId";

            var user = new User() { Id = userId };
            var amount = 1m;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(userId))
                .Returns(user);

            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);

            // Act
            userService.TopUpUserBalance(userId, amount);

            // Assert
            Assert.AreEqual(1, user.Balance);
        }

        [Test]
        public void Call_SaveChanges_Of_BrandviserData_Once()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userId";

            var user = new User() { Id = userId };
            var amount = 1m;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(userId))
                .Returns(user);

            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);


            // Act
            userService.TopUpUserBalance(userId, amount);

            // Assert
            brandviserData.Verify(u => u.SaveChanges(), Times.Once());
        }
    }
}
