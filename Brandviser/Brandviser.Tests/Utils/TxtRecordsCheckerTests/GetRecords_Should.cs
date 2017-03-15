using System;
using Brandviser.Common;
using NUnit.Framework;

namespace Brandviser.Tests.Utils.TxtRecordsCheckerTests
{
    [TestFixture]
    public class GetRecords_Should
    {
        [Test]
        [TestCase(null)]
        public void Throw_When_ArgumentDomain_IsNull(string domain)
        {
            // Arrange
            var txtRecordsChecker = new TxtRecordsChecker();

            // Act & Assert
            Assert.Throws(typeof(ArgumentNullException),
                 () => txtRecordsChecker.GetRecords(domain));
        }

        [Test]
        [TestCase("")]
        public void Throw_When_ArgumentDomain_IsEmpty(string domain)
        {
            // Arrange
            var txtRecordsChecker = new TxtRecordsChecker();

            // Act & Assert
            Assert.Throws(typeof(ArgumentException),
                 () => txtRecordsChecker.GetRecords(domain));
        }
    }
}
