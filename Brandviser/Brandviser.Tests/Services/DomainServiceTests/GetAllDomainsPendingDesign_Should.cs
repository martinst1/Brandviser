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
    public class GetAllDomainsPendingDesign_Should
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


            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.GetAllDomainsPendingDesign();

            // Assert
            mockedDomainsRepository.Verify(d => d.All, Times.Once());
        }

        [Test]
        public void Return_Correct_Results()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            var domainCollection = new List<Domain>
            {
                new Domain {StatusId = 6},
                new Domain { StatusId = 6},
                new Domain { StatusId = 5}

            }.AsQueryable();

            mockedDomainsRepository.Setup(d => d.All).Returns(domainCollection);
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            var expected = domainService.GetAllDomainsPendingDesign().ToList();

            // Assert
            Assert.That(expected.Count == 2);
            Assert.That(!expected.Contains(domainCollection.ToList()[2]));
        }
    }
}
