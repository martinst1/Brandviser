using System;
using Brandviser.Common;
using Brandviser.Common.Constants;
using Brandviser.Common.Contracts;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Utils.WhoisTests
{
    [TestFixture]
    public class LookupDotComDomain_Should
    {
        [Test]
        public void Throw_Exception_When_DomainName_Is_Null()
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act & Assert
            Assert.Throws(typeof(ArgumentNullException),
                () => whois.LookupDotComDomain(null, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }
    }
}
