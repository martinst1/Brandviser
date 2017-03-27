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
    public class ApproveDomain_Should
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
            var name = "name";
            decimal? price = 1;
            var domain = new Domain() { Name = name };
            var collection = new List<Domain>();
            collection.Add(domain);

            mockedDomainsRepository.Setup(r => r.All).Returns(collection.AsQueryable<Domain>());
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.ApproveDomain(name, price);

            // Assert
            mockedDomainsRepository.Verify(d => d.All, Times.Once());
        }

        [Test]
        public void Call_DateTimeProvider_GetCurrentTime_Once()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();
            var name = "name";
            decimal? price = 1;
            var domain = new Domain() { Name = name };
            var collection = new List<Domain>();
            collection.Add(domain);

            mockedDomainsRepository.Setup(r => r.All).Returns(collection.AsQueryable<Domain>());
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);


            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.ApproveDomain(name, price);

            // Assert
            dateTimeProvider.Verify(d => d.GetCurrentTime(), Times.Once());
        }

        [Test]
        public void Repository_Update_IsCalledWith_CorrectObject()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();
            var name = "name";
            decimal? price = 1;
            var domain = new Domain() { Name = name };
            var collection = new List<Domain>();
            collection.Add(domain);
            var dateTime = new DateTime(17, 1, 1);

            dateTimeProvider.Setup(d => d.GetCurrentTime()).Returns(dateTime);
            mockedDomainsRepository.Setup(r => r.All).Returns(collection.AsQueryable<Domain>());
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);


            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            var expectedStatus = 3;

            // Act
            domainService.ApproveDomain(name, price);

            // Assert
            mockedDomainsRepository.Verify(d => d.Update(domain), Times.Once());
            Assert.AreEqual(expectedStatus, domain.StatusId);
            Assert.AreEqual(dateTime, domain.UpdatedAt);
            Assert.AreEqual(price, domain.Price);
            Assert.AreEqual(price, domain.OriginalOwnerCustomPrice);
        }

        [Test]
        public void Call_BrandviserData_SaveChanges_Once()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();
            var name = "name";
            decimal? price = 1;
            var domain = new Domain() { Name = name };
            var collection = new List<Domain>();
            collection.Add(domain);

            mockedDomainsRepository.Setup(r => r.All).Returns(collection.AsQueryable<Domain>());
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);


            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.ApproveDomain(name, price);

            // Assert
            brandviserData.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
