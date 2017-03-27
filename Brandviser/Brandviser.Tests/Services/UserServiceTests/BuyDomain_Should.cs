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
    public class BuyDomain_Should
    {
        [Test]
        public void Call_GetByStringId_Of_UserRepository_Once()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var domainRepository = new Mock<IEfRepository<Domain>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userid";
            var domainId = 1;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(It.IsAny<string>()))
                .Returns(new User() { Balance = 2m });
            domainRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(new Domain());
            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);
            brandviserData.Setup(b => b.Domains).Returns(domainRepository.Object);


            // Act
            userService.BuyDomain(userId, domainId);

            // Assert
            userRepository.Verify(u => u.GetByStringId(userId), Times.Once());
        }

        [Test]
        public void Call_GetById_Of_DomainRepository_Once()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var domainRepository = new Mock<IEfRepository<Domain>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userid";
            var domainId = 1;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(It.IsAny<string>()))
                .Returns(new User() { Balance = 2m });
            domainRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(new Domain());
            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);
            brandviserData.Setup(b => b.Domains).Returns(domainRepository.Object);


            // Act
            userService.BuyDomain(userId, domainId);

            // Assert
            domainRepository.Verify(u => u.GetById(domainId), Times.Once());
        }

        [Test]
        public void Call_GetCurrentTime_Of_DateTimeProvider_Once()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var domainRepository = new Mock<IEfRepository<Domain>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userid";
            var domainId = 1;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(It.IsAny<string>()))
                .Returns(new User() { Balance = 2m });
            domainRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(new Domain());
            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);
            brandviserData.Setup(b => b.Domains).Returns(domainRepository.Object);


            // Act
            userService.BuyDomain(userId, domainId);

            // Assert
            dateTimeProvider.Verify(u => u.GetCurrentTime(), Times.Once());
        }

        [Test]
        public void Sets_Data_Correctly()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var domainRepository = new Mock<IEfRepository<Domain>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userid";
            var domainId = 1;
            var dateTime = new DateTime(17, 1, 1);

            var domain = new Domain();
            var user = new User();
            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            dateTimeProvider.Setup(d => d.GetCurrentTime()).Returns(dateTime);
            userRepository.Setup(r => r.GetByStringId(It.IsAny<string>()))
                .Returns(user);
            domainRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(domain);
            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);
            brandviserData.Setup(b => b.Domains).Returns(domainRepository.Object);


            // Act
            userService.BuyDomain(userId, domainId);

            // Assert
            Assert.AreEqual(domain, user.BuyerDomains.First());
            Assert.AreEqual(dateTime, domain.SoldOn);
        }

        [Test]
        public void Call_SaveChanges_Of_BrandviserData_Once()
        {
            // Arrange
            var brandviserData = new Mock<IBrandviserData>();
            var userRepository = new Mock<IEfRepository<User>>();
            var domainRepository = new Mock<IEfRepository<Domain>>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var userId = "userid";
            var domainId = 1;

            var userService = new UserService(brandviserData.Object, dateTimeProvider.Object);

            userRepository.Setup(r => r.GetByStringId(It.IsAny<string>()))
                .Returns(new User() { Balance = 2m });
            domainRepository.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(new Domain());
            brandviserData.Setup(b => b.Users).Returns(userRepository.Object);
            brandviserData.Setup(b => b.Domains).Returns(domainRepository.Object);


            // Act
            userService.BuyDomain(userId, domainId);

            // Assert
            brandviserData.Verify(u => u.SaveChanges(), Times.Once());
        }
    }
}
