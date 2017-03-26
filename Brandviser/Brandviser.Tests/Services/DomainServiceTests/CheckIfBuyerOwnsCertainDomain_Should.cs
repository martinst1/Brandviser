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
    public class CheckIfBuyerOwnsCertainDomain_Should
    {
        [Test]
        public void Call_Domain_Repository_Once()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            mockedDomainsRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Domain());
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var actualDomainId = 1;
            var actualbuyerId = "buyer id";

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.CheckIfBuyerOwnsCertainDomain(actualDomainId, actualbuyerId);

            // Assert
            mockedDomainsRepository.Verify(d => d.GetById(actualDomainId), Times.Once());
        }

        [Test]
        public void Return_True()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            mockedDomainsRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Domain() { BuyerId = "buyer id" });
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var actualDomainId = 1;
            var actualbuyerId = "buyer id";

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            var actualResult = domainService.CheckIfBuyerOwnsCertainDomain(actualDomainId, actualbuyerId);

            // Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void Return_False()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            mockedDomainsRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(new Domain() { BuyerId = "wrong id" });
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var actualDomainId = 1;
            var actualbuyerId = "buyer id";

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            var actualResult = domainService.CheckIfBuyerOwnsCertainDomain(actualDomainId, actualbuyerId);

            // Assert
            Assert.IsFalse(actualResult);
        }
    }
}
