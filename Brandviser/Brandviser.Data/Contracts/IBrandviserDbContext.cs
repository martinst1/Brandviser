using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Brandviser.Data.Models;

namespace Brandviser.Data.Contracts
{
    public interface IBrandviserDbContext : IDisposable
    {
        IDbSet<Domain> Domains { get; }

        IDbSet<Status> Statuses { get; }

        IDbSet<User> Users { get; }

        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
