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
    public class Search_Should
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
            var searchedTerm = "testterm";

            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.Search(searchedTerm);

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

            var searchedText = "test";

            var correctUser = new User() { FirstName = "test", LastName = "test" };
            var incorrectUser = new User() { FirstName = "wrong", LastName = "wrong" };


            var domainCollection = new List<Domain>
            {
                new Domain { Name = "testname", Description = "wrong", StatusId = 4, User = incorrectUser, Designer=incorrectUser},
                new Domain { Name = "wrong", Description = "testdescription", StatusId = 4, User= incorrectUser, Designer=incorrectUser},
                new Domain { Name= "wrong", Description = "wrong" , StatusId = 4, User = correctUser, Designer = incorrectUser},
                new Domain { Name= "wrong", Description = "wrong" , StatusId = 4, User = incorrectUser, Designer = correctUser}

            }.AsQueryable();
            mockedDomainsRepository.Setup(d => d.All).Returns(domainCollection);
            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            var actual = domainCollection.ToList();
            // Act
            var expected = domainService.Search(searchedText);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
