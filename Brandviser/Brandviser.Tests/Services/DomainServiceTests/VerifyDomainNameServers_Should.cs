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
    public class VerifyDomainNameServers_Should
    {
        [Test]
        public void Call_DomainRepository_Once()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);
            mockedDomainsRepository.Setup(d => d.All).Returns(new List<Domain>()
            {
                new Domain()
                {
                    Name = "name"
                }
            }.AsQueryable());
            whois.Setup(w => w.LookupDotComDomain(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns("returned value");

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.VerifyDomainNameNameservers("name", "nameserver1", "nameserver2");

            // Assert
            mockedDomainsRepository.Verify(d => d.All, Times.Once());
        }

        [Test]
        public void Call_Whois_Once()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);
            mockedDomainsRepository.Setup(d => d.All).Returns(new List<Domain>()
            {
                new Domain()
                {
                    Name = "name"
                }
            }.AsQueryable());
            whois.Setup(w => w.LookupDotComDomain(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns("returned value");

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.VerifyDomainNameNameservers("name", "nameserver1", "nameserver2");

            // Assert
            whois.Verify(w => w.LookupDotComDomain(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once());
        }

        [Test]
        [TestCase("nameserver1 nameserver2")]
        [TestCase("nameserver1")]
        [TestCase("nameserver2")]
        [TestCase("namEserver1 naMeserver2")]
        [TestCase("NAMESERVER1")]
        [TestCase("NAMESERVER2")]
        [TestCase("NAMESERVER1 NAMESERVER2")]
        public void Returns_Correct_True_Result(string nameservers)
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);
            mockedDomainsRepository.Setup(d => d.All).Returns(new List<Domain>()
            {
                new Domain()
                {
                    Name = "name"
                }
            }.AsQueryable());
            whois.Setup(w => w.LookupDotComDomain(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(nameservers);

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            var domainIsVerified= domainService.VerifyDomainNameNameservers("name", "nameserver1", "nameserver2");

            // Assert
            Assert.IsTrue(domainIsVerified);
        }

        [Test]
        public void Returns_Correct_False_Result()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);
            mockedDomainsRepository.Setup(d => d.All).Returns(new List<Domain>()
            {
                new Domain()
                {
                    Name = "name"
                }
            }.AsQueryable());
            whois.Setup(w => w.LookupDotComDomain(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns("");

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            var domainIsVerified = domainService.VerifyDomainNameNameservers("name", "nameserver1", "nameserver2");

            // Assert
            Assert.IsFalse(domainIsVerified);
        }
    }
}
