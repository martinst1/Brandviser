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
    public class GetDomainByName_Should
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

            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var actualName = "test name";

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.GetSellerSoldDomainsByUserId(actualName);

            // Assert
            mockedDomainsRepository.Verify(d => d.All, Times.Once());
        }

        [Test]
        public void Return_Correct_Domain()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();
            var correctName = "testname";
            var domainCollection = new List<Domain>
            {
                new Domain {Name = "testname", StatusId = 1},
                new Domain {Name = "testname2", StatusId = 1},
                new Domain {Name = "testname2", StatusId = 1}

            }.AsQueryable();
            mockedDomainsRepository.Setup(d => d.All).Returns(domainCollection);
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            var actual = domainCollection.ToList()[0];
            // Act
            var expected = domainService.GetDomainByName(correctName);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
