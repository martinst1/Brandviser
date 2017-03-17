using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;

namespace Brandviser.Data.Contracts
{
    public interface IBrandviserData
    {
        IEfRepository<Domain> Domains { get; }

        IEfRepository<Status> Statuses { get; }

        IEfRepository<User> Users { get; }

        void SaveChanges();
    }
}
