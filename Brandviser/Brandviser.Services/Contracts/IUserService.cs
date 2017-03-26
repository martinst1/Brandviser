using Brandviser.Data.Models;

namespace Brandviser.Services.Contracts
{
    public interface IUserService
    {
        User GetUserByStringId(string id);

        bool CheckIfBuyerHasEnoughMoney(string buyerId, decimal neededAmount);

        void BuyDomain(string buyerId, int domainId);

        void TransferAmountFromBuyerToSeller(string buyerId, decimal amount, string sellerId);

        void TopUpUserBalance(string userId, decimal amount);
    }
}
