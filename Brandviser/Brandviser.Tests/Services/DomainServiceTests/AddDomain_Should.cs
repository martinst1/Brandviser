using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Common.Contracts;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Brandviser.Factories;
using Brandviser.Services;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Services.DomainServiceTests
{
    [TestFixture]
    public class AddDomain_Should
    {
        [Test]
        public void Call_BrandviserDataDomainsDataRepositoryOnce_With_CorrectPassedParams()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var bradviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var name = "test.com";
            var description = "test description";
            var userId = "test string user id";
            var mockedRepository = new Mock<IEfRepository<Domain>>();
            var fakedCurrentTime = new DateTime(2017, 1, 1);

            dateTimeProvider.Setup(d => d.GetCurrentTime()).Returns(fakedCurrentTime);

            // 1 = pending, first obtained status of every domain
            var realStatusId = 1;
            var actualDomain = new Domain()
            {
                UserId = userId,
                Name = name,
                StatusId = realStatusId,
                Description = description,
                CreatedAt = fakedCurrentTime
            };

            domainFactory.Setup(d => d.CreateDomain(It.IsAny<string>(),
             It.IsAny<string>(), It.IsAny<int>(),
             It.IsAny<string>(), It.IsAny<DateTime>())).Returns(actualDomain);

            bradviserData.Setup(b => b.Domains).Returns(mockedRepository.Object);

            var domainService = new DomainService(bradviserData.Object, domainFactory.Object,
               dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.AddDomain(name, description, userId);

            // Assert
            mockedRepository.Verify(r => r.Add(actualDomain), Times.Once());
        }

        [Test]
        public void Call_BrandviserSaveChanges_Once()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var bradviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var domainsRepository = new Mock<IEfRepository<Domain>>();

            var domainService = new DomainService(bradviserData.Object, domainFactory.Object,
               dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            bradviserData.Setup(d => d.Domains).Returns(domainsRepository.Object);

            dateTimeProvider.Setup(d => d.GetCurrentTime()).Returns(new DateTime(17, 1, 1));
            domainFactory.Setup(d =>
            d.CreateDomain(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(new Domain());
            // Act
            domainService.AddDomain("name", "description", "userId");

            // Assert
            bradviserData.Verify(b => b.SaveChanges(), Times.Once());
        }

        [Test]
        public void Call_DateTimeProvider_GetCurrentTime_Once()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var bradviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var domainsRepository = new Mock<IEfRepository<Domain>>();

            bradviserData.Setup(d => d.Domains).Returns(domainsRepository.Object);

            var domainService = new DomainService(bradviserData.Object, domainFactory.Object,
               dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            dateTimeProvider.Setup(d => d.GetCurrentTime()).Returns(new DateTime(17, 1, 1));
            domainFactory.Setup(d =>
            d.CreateDomain(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(new Domain());
            // Act
            domainService.AddDomain("name", "description", "userId");

            // Assert
            dateTimeProvider.Verify(d => d.GetCurrentTime(), Times.Once());
        }
    }
}
