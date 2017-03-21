using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Common.Contracts;

namespace Brandviser.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
