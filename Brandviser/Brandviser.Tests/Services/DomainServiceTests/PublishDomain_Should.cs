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
    public class PublishDomain_Should
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
            var name = "test name";

            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);
            mockedDomainsRepository.Setup(d => d.All).Returns(new List<Domain>()
            {
                new Domain()
                {
                    Name = name
                }
            }.AsQueryable());

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.PublishDomain(name);

            // Assert
            mockedDomainsRepository.Verify(d => d.All, Times.Once());
        }

        [Test]
        public void Change_Domain_Status_To_4Published()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();
            var name = "test name";
            var collection = new List<Domain>()
            {
                new Domain()
                {
                    Name = name
                }
            };
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);
            mockedDomainsRepository.Setup(d => d.All).Returns(collection.AsQueryable());

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.PublishDomain(name);

            // Assert
            Assert.That(collection[0].StatusId == 4);
        }
    }
}
