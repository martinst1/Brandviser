using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandviser.Common.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentTime();
    }
}
