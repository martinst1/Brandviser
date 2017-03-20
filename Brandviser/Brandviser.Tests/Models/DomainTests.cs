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
    public class DomainTests
    {
        [Test]
        public void ConstructorShouldCreateDomain_WithoutParams()
        {
            // Act & Assert
            var domain = new Domain();

            Assert.IsInstanceOf<Domain>(domain);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new Domain());
        }

        [Test]
        public void ConstructorShouldAssign_NewGuid_ToVerificationProperty_OnModelCreate()
        {
            // Arrange & Act
            var domain = new Domain();

            // Assert
            Assert.That(domain.VerificationCode, Is.Not.Null.Or.Empty.And.InstanceOf<Guid>());
        }

        [Test]
        public void Domain_Should_Set_Properties_Correctly_With_CorrectData()
        {
            // Arrange
            var id = 1;
            var name = "test.com";
            var userId = "stringId2";
            var buyerId = "stringId3";
            var designerId = "stringId1";
            var price = 11.11m;
            var originalOwnerCustomPrice = 11m;
            var statusId = 1;
            var description = "test description";
            var soldOn = new DateTime(2012, 12, 1);
            var createdAt = new DateTime(2012, 10, 1);
            var updatedAt = new DateTime(2012, 11, 1);
            var logoUrl = "//testurl";

            // Act
            var domain = new Domain()
            {
                Id = id,
                Name = name,
                UserId = userId,
                BuyerId = buyerId,
                DesignerId = designerId,
                Price = price,
                OriginalOwnerCustomPrice = originalOwnerCustomPrice,
                StatusId = statusId,
                Description = description,
                SoldOn = soldOn,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
                LogoUrl = logoUrl
            };

            // Assert
            Assert.AreEqual(domain.Id, id);
            Assert.AreEqual(domain.Name, name);
            Assert.AreEqual(domain.UserId, userId);
            Assert.AreEqual(domain.BuyerId, buyerId);
            Assert.AreEqual(domain.DesignerId, designerId);
            Assert.AreEqual(domain.Price, price);
            Assert.AreEqual(domain.OriginalOwnerCustomPrice, originalOwnerCustomPrice);
            Assert.AreEqual(domain.StatusId, statusId);
            Assert.AreEqual(domain.Description, description);
            Assert.AreEqual(domain.SoldOn, soldOn);
            Assert.AreEqual(domain.CreatedAt, createdAt);
            Assert.AreEqual(domain.LogoUrl, logoUrl);
        }
    }
}
