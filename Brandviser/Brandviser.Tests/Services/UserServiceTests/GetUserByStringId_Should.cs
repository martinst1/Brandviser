using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Brandviser.Services;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Services.UserServiceTests
{
    [TestFixture]
    public class GetUserByStringId_Should
    {
        [Test]
        [TestCase("stringid1")]
        [TestCase("stringid2")]
        public void Call_GetByStringId_Of_UserRepository_Once(string id)
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var userService = new UserService(brandviserData.Object);

            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);

            // Act
            userService.GetUserByStringId(id);

            // Assert
            userRepository.Verify(u => u.GetByStringId(id), Times.Once());
        }
    }
}
