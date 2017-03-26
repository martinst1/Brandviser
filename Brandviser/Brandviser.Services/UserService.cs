using System.Collections.Generic;
using Brandviser.Common.Contracts;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Bytes2you.Validation;

namespace Brandviser.Services
{
    public class UserService : IUserService
    {
        private readonly IBrandviserData brandviserData;
        private readonly IDateTimeProvider dateTimeProvider;

        public UserService(IBrandviserData brandviserData, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(brandviserData, nameof(IBrandviserData)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.brandviserData = brandviserData;
            this.dateTimeProvider = dateTimeProvider;
        }

        public User GetUserByStringId(string id)
        {
            var user = this.brandviserData.Users.GetByStringId(id);

            return user;
        }

        public bool CheckIfBuyerHasEnoughMoney(string buyerId, decimal neededAmount)
        {
            var user = this.brandviserData.Users.GetByStringId(buyerId);

            var userHasEnoughMoney = user.Balance >= neededAmount;

            if (userHasEnoughMoney)
            {
                return true;
            }

            return false;
        }

        public void BuyDomain(string buyerId, int domainId)
        {
            var user = this.brandviserData.Users.GetByStringId(buyerId);

            var domain = this.brandviserData.Domains.GetById(domainId);

            domain.SoldOn = this.dateTimeProvider.GetCurrentTime();

            user.BuyerDomains.Add(domain);

            this.brandviserData.SaveChanges();
        }

        public void TransferAmountFromBuyerToSeller(string buyerId, decimal amount, string sellerId)
        {
            var buyer = this.brandviserData.Users.GetByStringId(buyerId);
            buyer.Balance -= amount;
            this.brandviserData.SaveChanges();

            var seller = this.brandviserData.Users.GetByStringId(sellerId);
            seller.Balance += amount;
            this.brandviserData.SaveChanges();
        }

        public void TopUpUserBalance(string userId, decimal amount)
        {
            var user = this.brandviserData.Users.GetByStringId(userId);

            user.Balance += amount;

            this.brandviserData.SaveChanges();
        }
    }
}