using System;
using Brandviser.Common;
using Brandviser.Common.Contracts;
using Moq;
using NUnit.Framework;

namespace Brandviser.Tests.Utils.WhoisTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_WithExpectedMessage_When_ISocket_IsNull()
        {
            // Arrange
            string expectedContainingString = nameof(ISocket);

            // Act & Assert
            var output = Assert.Throws<ArgumentNullException>(() => new Whois(null));

            StringAssert.Contains(expectedContainingString, output.Message);
        }

        [Test]
        public void NotThrow_When_ISocket_IsPassed()
        {
            // Arrange
            var socketMock = new Mock<ISocket>();

            // Act & Assert
            Assert.DoesNotThrow(() => new Whois(socketMock.Object));
        }

        [Test]
        public void CreateInstance_of_IWhois_When_CorrectArgument_IsPassed()
        {
            // Arrange
            var socketMock = new Mock<ISocket>();
            var expected = typeof(IWhois);

            // Act 
            var actual = new Whois(socketMock.Object);

            // Assert
            Assert.IsInstanceOf(expected, actual);
        }
    }
}
