using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Brandviser.Services.Contracts;
using Bytes2you.Validation;

namespace Brandviser.Services
{
    public class UserService : IUserService
    {
        private readonly IBrandviserData brandviserData;

        public UserService(IBrandviserData brandviserData)
        {
            Guard.WhenArgument(brandviserData, nameof(IBrandviserData)).IsNull().Throw();

            this.brandviserData = brandviserData;
        }

        public User GetUserByStringId(string id)
        {
            var user = this.brandviserData.Users.GetByStringId(id);

            return user;
        }
    }
}
