using Brandviser.Data.Models;

namespace Brandviser.Services.Contracts
{
    public interface IUserService
    {
        User GetUserByStringId(string id);
    }
}
