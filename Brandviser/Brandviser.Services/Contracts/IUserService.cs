using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;

namespace Brandviser.Services.Contracts
{
    public interface IUserService
    {
        User GetUserByGuidId(string id);
    }
}
