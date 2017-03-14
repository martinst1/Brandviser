using System;
using System.Text;
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

        [Test]
        public void Throw_Exception_When_DomainName_Starts_With_Hyphen()
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var domainName = "-hyphen.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act & Assert
            Assert.Throws(typeof(ArgumentException),
                () => whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }

        [Test]
        [TestCase("*domain.com")]
        [TestCase("_domain.com")]
        [TestCase("(domain.com")]
        public void Throw_Exception_When_DomainName_Contains_Illegal_Characters_AndDoesNotMatchPattern(string domain)
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var domainName = domain;
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act & Assert
            Assert.Throws(typeof(ArgumentException),
                () => whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }

        [Test]
        [TestCase("domain.net")]
        [TestCase("domain.org")]
        [TestCase("domain.ly")]
        public void Throw_Exception_When_DomainName_DoesNotEndWith_DotCom(string domain)
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var domainName = domain;
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act & Assert
            Assert.Throws(typeof(ArgumentException),
                () => whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }

        [Test]
        public void Throw_Exception_When_DomainNameLength_isOutOfRange()
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var domainName = new String('a', 255) + ".com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act & Assert
            Assert.Throws(typeof(ArgumentOutOfRangeException),
                () => whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(65536)]
        public void Throw_Exception_When_Port_isOutOfRange(int tcpPort)
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var domainName = "test.com";
            var port = tcpPort;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act & Assert
            Assert.Throws(typeof(ArgumentOutOfRangeException),
                () => whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }

        [Test]
        public void Throw_Exception_When_WhoisServer_Is_Null()
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var domain = "test.com";
            var port = WhoisConstants.Port;
            string whoisServer = null;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act & Assert
            Assert.Throws(typeof(ArgumentNullException),
                () => whois.LookupDotComDomain(domain, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }

        [Test]
        public void Throw_Exception_When_WhoisServerLookupQueryPrefix_Is_Null()
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var domain = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer; ;
            string whoisServerLookupQueryPrefix = null;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act & Assert
            Assert.Throws(typeof(ArgumentNullException),
                () => whois.LookupDotComDomain(domain, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(10000)]
        public void Throw_Exception_When_ResponseBufferSizeInBytes_isOutOfRange(int bufferSize)
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var whois = new Whois(socketMock.Object);
            var domainName = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = bufferSize;

            // Act & Assert
            Assert.Throws(typeof(ArgumentOutOfRangeException),
                () => whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes));
        }

        [Test]
        public void Call_ISocket_ConnectMethod_Once()
        {
            // Arrange
            var socketStub = new Mock<ISocket>();
            var whois = new Whois(socketStub.Object);
            var domainName = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act
            whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes);

            // Assert
            socketStub.Verify(s => s.Connect(WhoisConstants.WhoisServer, port), Times.Once());
        }

        [Test]
        public void Call_ISocket_SendMethod_Once()
        {
            // Arrange
            var socketStub = new Mock<ISocket>();
            var whois = new Whois(socketStub.Object);
            var domainName = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;
            byte[] query = Encoding.ASCII.GetBytes(whoisServerLookupQueryPrefix + domainName + Environment.NewLine);

            // Act
            whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes);

            // Assert
            socketStub.Verify(s => s.Send(query), Times.Once());
        }

        [Test]
        public void Call_ISocket_ReceiveMethod_Once()
        {
            // Arrange
            var socketStub = new Mock<ISocket>();
            var whois = new Whois(socketStub.Object);
            var domainName = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;
            byte[] responseBytes = new byte[responseBufferSizeInBytes];
            // Act
            whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes);

            // Assert
            socketStub.Verify(s => s.Receive(responseBytes), Times.Once());
        }

        [Test]
        public void Call_ISocket_ConnectMethod_WithExpectedParams()
        {
            // Arrange
            var socketStub = new Mock<ISocket>();
            var whois = new Whois(socketStub.Object);
            var domainName = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act
            whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes);

            // Assert
            socketStub.Verify(s => s.Connect(WhoisConstants.WhoisServer, port));
        }

        [Test]
        public void Call_ISocket_SendMethod_WithExpectedParams()
        {
            // Arrange
            var socketStub = new Mock<ISocket>();
            var whois = new Whois(socketStub.Object);
            var domainName = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;
            var responseBytes = new byte[responseBufferSizeInBytes];
            byte[] query = Encoding.ASCII.GetBytes(whoisServerLookupQueryPrefix + domainName + Environment.NewLine);
            // Act
            whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes);

            // Assert
            socketStub.Verify(s => s.Send(query));
        }

        [Test]
        public void Call_ISocket_ReceiveMethod_WithExpectedParams()
        {
            // Arrange
            var socketStub = new Mock<ISocket>();
            var whois = new Whois(socketStub.Object);
            var domainName = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;
            var responseBytes = new byte[responseBufferSizeInBytes];
            // Act
            whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes);

            // Assert
            socketStub.Verify(s => s.Receive(responseBytes));
        }
        [Test]
        public void Return_CorrectResults()
        {
            // Arrange
            string expectedMessage = "test.com whois message";

            var socketStub = new FakeSocket(expectedMessage);
            var whois = new Whois(socketStub);
            var domainName = "test.com";
            var port = WhoisConstants.Port;
            var whoisServer = WhoisConstants.WhoisServer;
            var whoisServerLookupQueryPrefix = WhoisConstants.WhoisServerLookupQueryPrefix;
            var responseBufferSizeInBytes = WhoisConstants.RecommendedBufferSizeInBytes;

            // Act
            string actualMessage = whois.LookupDotComDomain(domainName, port, whoisServer, whoisServerLookupQueryPrefix, responseBufferSizeInBytes);

            // Assert
            StringAssert.Contains(expectedMessage, actualMessage);
        }
    }
}
