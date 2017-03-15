using Brandviser.Common;
using Brandviser.Common.Contracts;
using NUnit.Framework;

namespace Brandviser.Tests.Utils.TxtRecordsCheckerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Create_InstanceOf_ITxtRecordsChecker()
        {
            // Act
            var txtRecordsChecker = new TxtRecordsChecker();

            // Assert
            Assert.IsInstanceOf(typeof(ITxtRecordsChecker), txtRecordsChecker);
        }
    }
}
