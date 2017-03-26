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
    public class UpdateDomainToBought_Should
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

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.UpdateDomainToBought(actualDomainId);

            // Assert
            mockedDomainsRepository.Verify(d => d.GetById(actualDomainId), Times.Once());
        }

        [Test]
        public void Changes_Id_As_Stated()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();
            var domain = new Domain();

            mockedDomainsRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(domain);
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var actualDomainId = 1;

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.UpdateDomainToBought(actualDomainId);

            // Assert
            Assert.AreEqual(5, domain.StatusId);
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
            var domain = new Domain();

            mockedDomainsRepository.Setup(m => m.GetById(It.IsAny<int>())).Returns(domain);
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var actualDomainId = 1;

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.UpdateDomainToBought(actualDomainId);

            // Assert
            brandviserData.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
