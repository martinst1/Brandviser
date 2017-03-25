using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandviser.Web.Helpers.Contracts
{
    public interface ILoggedInUser
    {
        string GetUserId();
    }
}
