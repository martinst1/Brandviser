using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using NUnit.Framework;

namespace Brandviser.Tests.Models
{
    [TestFixture]
    public class StatusTests
    {
        [Test]
        public void ConstructorShouldCreateStatus_WithoutParams()
        {
            // Act & Assert
            var status = new Status();

            Assert.IsInstanceOf<Status>(status);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new Status());
        }

        [Test]
        public void ConstructorShouldCreateStatus_WithCorrectParams()
        {
            // Act & Assert
            var status = new Status("name");

            Assert.IsInstanceOf<Status>(status);
        }

        [Test]
        public void StatusShouldCreate_HashsetOf_Domains_WhenInitialized()
        {
            // Arrange
            var status = new Status();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Domain>), status.Domains);
        }

        [Test]
        public void StatusShouldCreate_EmptyHashsetOf_Domains_WhenInitialized()
        {
            // Arrange
            var status = new Status();
            var expectedCollection = new HashSet<Domain>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, status.Domains);
        }

        [Test]
        public void Status_ShouldBeCreated_WithValidParams()
        {
            // Arrange
            var id = 1;
            var name = "status";

            // Act
            var status = new Status()
            {
                Id = 1,
                Name = name
            };

            // Assert
            Assert.AreEqual(id, status.Id);
            Assert.AreEqual(name, status.Name);
        }
    }
}
