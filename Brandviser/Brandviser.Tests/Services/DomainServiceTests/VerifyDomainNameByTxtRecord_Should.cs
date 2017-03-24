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
    public class VerifyDomainNameByTxtRecord_Should
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
            txtRecordsChecker.Setup(t => t.GetRecords("name")).Returns("");

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.VerifyDomainNameByTxtRecord("name");

            // Assert
            mockedDomainsRepository.Verify(d => d.All, Times.Once());
        }

        [Test]
        public void Call_TxtRecords_Once()
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
            txtRecordsChecker.Setup(t => t.GetRecords("name")).Returns("");

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            domainService.VerifyDomainNameByTxtRecord("name");

            // Assert
            txtRecordsChecker.Verify(d => d.GetRecords("name"), Times.Once());
        }

        [Test]
        [TestCase("3BAE8BB0-E085-481C-98A3-1330864F1E59")]
        [TestCase("1B57D970-94BE-4CA2-82C0-9A1956D81834")]
        [TestCase("35E06EFD-A1F3-424A-B5AF-C7D52CBB91E9")]
        public void Returns_Correct_True_Result(string verificationCode)
        {
            // Arrange
            var domainFactory = new Mock<IDomainFactory>();
            var brandviserData = new Mock<IBrandviserData>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var whois = new Mock<IWhois>();
            var txtRecordsChecker = new Mock<ITxtRecordsChecker>();
            var mockedDomainsRepository = new Mock<IEfRepository<Domain>>();

            brandviserData.Setup(b => b.Domains).Returns(mockedDomainsRepository.Object);
            var collection = new List<Domain>()
            {
                new Domain()
                {
                    Name = "name"
                }
            };
            collection[0].VerificationCode = Guid.Parse(verificationCode);
            mockedDomainsRepository.Setup(d => d.All).Returns(collection.AsQueryable());

            txtRecordsChecker.Setup(t => t.GetRecords(collection.ToList()[0].Name))
                .Returns(collection.ToList()[0].VerificationCode.ToString());

            var domainService = new DomainService(brandviserData.Object,
                domainFactory.Object, dateTimeProvider.Object, whois.Object, txtRecordsChecker.Object);

            // Act
            var domainIsVerified = domainService.VerifyDomainNameByTxtRecord("name");

            // Assert
            Assert.IsTrue(domainIsVerified);
        }
    }
}
