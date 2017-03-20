using System;
using System.Collections.Generic;
using Brandviser.Data.Contracts;
using Brandviser.Data.Models;
using Brandviser.Data.Repositories;
using Bytes2you.Validation;

namespace Brandviser.Data
{
    public class BrandviserData : IBrandviserData
    {
        private readonly IBrandviserDbContext dbContext;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public BrandviserData(IBrandviserDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, nameof(IBrandviserDbContext)).IsNull().Throw();

            this.dbContext = dbContext;
        }

        public IEfRepository<Domain> Domains
        {
            get
            {
                return this.GetRepository<Domain>();
            }
        }

        public IEfRepository<Status> Statuses
        {
            get
            {
                return this.GetRepository<Status>();
            }
        }

        public IEfRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        private IEfRepository<T> GetRepository<T>()
         where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                this.repositories.Add(typeof(T), Activator.CreateInstance(typeof(EfRepository<T>), this.dbContext));
            }
            return (IEfRepository<T>)this.repositories[typeof(T)];
        }
    }
}
