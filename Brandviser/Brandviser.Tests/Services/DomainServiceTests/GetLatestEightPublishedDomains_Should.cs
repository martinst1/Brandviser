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
    public class GetLatestEightPublishedDomains_Should
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
            domainService.GetLatestEightPublishedDomains();

            // Assert
            mockedDomainsRepository.Verify(d => d.All, Times.Once());
        }


        [Test]
        public void Return_Correct_Domains()
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            var updatedAt = new DateTime(17, 1, 1);

            var correctUser = new User() { FirstName = "test", LastName = "test" };
            var incorrectUser = new User() { FirstName = "wrong", LastName = "wrong" };


            var domainCollection = new List<Domain>
            {
                new Domain { StatusId = 4, UpdatedAt = updatedAt.AddMilliseconds(1)},
                new Domain { StatusId = 4, UpdatedAt = updatedAt.AddMilliseconds(2)},
                new Domain { StatusId = 4, UpdatedAt = updatedAt.AddMilliseconds(3)},
                new Domain { StatusId = 4, UpdatedAt = updatedAt.AddMilliseconds(4)},
                new Domain { StatusId = 4, UpdatedAt = updatedAt.AddMilliseconds(5)},
                new Domain { StatusId = 4, UpdatedAt = updatedAt.AddMilliseconds(6)},
                new Domain { StatusId = 4, UpdatedAt = updatedAt.AddMilliseconds(7)},
                new Domain { StatusId = 4, UpdatedAt = updatedAt.AddMilliseconds(8)},
                new Domain { StatusId = 5, UpdatedAt = updatedAt}

            }.AsQueryable();
            mockedDomainsRepository.Setup(d => d.All).Returns(domainCollection);
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            var expected = domainCollection
                .Where(d => d.StatusId == 4)
                .Select(d => new Domain() { StatusId = d.StatusId, UpdatedAt = updatedAt })
                .OrderByDescending(d => d.UpdatedAt)
                .ToList();
            // Act
            var actual = domainService.GetLatestEightPublishedDomains();

            // Assert
            Assert.AreEqual(8, actual.Count());
            Assert.That(() => !actual.Any(d => d.StatusId == 5));
        }
    }
}
