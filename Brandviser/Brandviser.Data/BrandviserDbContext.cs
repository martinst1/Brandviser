using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brandviser.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Brandviser.Data
{
    public class BrandviserDbContext : IdentityDbContext<User>
    {
        public BrandviserDbContext()
            : base("BrandviserDb", throwIfV1Schema: false)
        {
        }

        public static BrandviserDbContext Create()
        {
            return new BrandviserDbContext();
        }
    }
}
