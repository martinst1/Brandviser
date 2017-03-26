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
    public class GetDomainById_Should
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

            var domainId = 1;

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.GetDomainById(domainId);

            // Assert
            mockedDomainsRepository.Verify(d => d.GetById(domainId), Times.Once());
        }

        [Test]
        public void Return_Correct_Result()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            var expected = new Domain() { Id = 1 };
            mockedDomainsRepository.Setup(m => m.GetById(1)).Returns(expected);
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var domainId = 1;

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            var actual = domainService.GetDomainById(domainId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Return_Null()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            var expected = new Domain() { Id = 1 };
            mockedDomainsRepository.Setup(m => m.GetById(2)).Returns(expected);
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var domainId = 1;

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            var actual = domainService.GetDomainById(domainId);

            // Assert
            Assert.AreEqual(null, actual);
        }
    }
}
