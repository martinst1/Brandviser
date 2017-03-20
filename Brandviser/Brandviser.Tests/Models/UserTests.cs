using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Brandviser.Data.Models.Constants;
using NUnit.Framework;

namespace Brandviser.Tests.Models
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void ConstructorShouldCreateUser_WithoutParams()
        {
            // Act & Assert
            var user = new User();

            Assert.IsInstanceOf<User>(user);
        }

        [Test]
        public void ConstructorShouldNotThrow_WithoutParams()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => new User());
        }

        [Test]
        public void UserShouldCreate_HashsetOf_Seller_Domains_WhenInitialized()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Domain>), user.SellerDomains);
        }

        [Test]
        public void UserShouldCreate_HashsetOf_Buyer_Domains_WhenInitialized()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Domain>), user.BuyerDomains);
        }

        [Test]
        public void UserShouldCreate_HashsetOf_Designer_Domains_WhenInitialized()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.IsInstanceOf(typeof(HashSet<Domain>), user.DesignerDomains);
        }

        [Test]
        public void UserShouldCreate_EmptyHashsetOf_Seller_Domains_WhenInitialized()
        {
            // Arrange
            var user = new User();
            var expectedCollection = new HashSet<Domain>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, user.SellerDomains);
        }

        [Test]
        public void UserShouldCreate_EmptyHashsetOf_Buyer_Domains_WhenInitialized()
        {
            // Arrange
            var user = new User();
            var expectedCollection = new HashSet<Domain>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, user.BuyerDomains);
        }

        [Test]
        public void UserShouldCreate_EmptyHashsetOf_Designer_Domains_WhenInitialized()
        {
            // Arrange
            var user = new User();
            var expectedCollection = new HashSet<Domain>();

            // Act & Assert
            CollectionAssert.AreEqual(expectedCollection, user.DesignerDomains);
        }

        [Test]
        public void User_ShouldBeCreated_WithValidParams()
        {
            // Arrange
            var balance = 1.11m;
            var firstname = "PeshoFirst";
            var lastname = "PeshoLast";
            var createdOn = DateTime.Now;

            // Act
            var user = new User()
            {
                Balance = balance,
                FirstName = firstname,
                LastName = lastname,
                CreatedOn = createdOn
            };

            // Assert
            Assert.AreEqual(balance, user.Balance);
            Assert.AreEqual(firstname, user.FirstName);
            Assert.AreEqual(lastname, user.LastName);
            Assert.AreEqual(createdOn, user.CreatedOn);
        }

    }
}
