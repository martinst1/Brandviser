using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Brandviser.Data.Contracts;
using Bytes2you.Validation;

namespace Brandviser.Data.Repositories
{
    public class EfRepository<T> : IEfRepository<T> where T : class
    {
        private readonly IBrandviserDbContext context;

        public EfRepository(IBrandviserDbContext context)
        {
            Guard.WhenArgument(context, nameof(IBrandviserDbContext)).IsNull().Throw();

            this.context = context;

            this.DbSet = this.context.Set<T>();
        }

        protected DbSet<T> DbSet { get; set; }

        public IQueryable<T> All
        {
            get { return this.DbSet; }
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet;
        }

        public T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public T GetByStringId(string id)
        {
            return this.DbSet.Find(id);
        }

        public void Add(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            DbEntityEntry entry;
            try
            {
                entry = this.context.Entry(entity);

            }
            catch (Exception)
            {
                throw;
            }

            if (entry.State == EntityState.Detached)
            {
                try
                {
                    this.DbSet.Attach(entity);

                }
                catch (Exception)
                {

                }
            }

            return entry;
        }
    }
}
