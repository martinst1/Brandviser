using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public User GetUserByGuidId(string id)
        {
            var user = this.brandviserData.Users.GetByGuidId(id);

            return user;
        }
    }
}
