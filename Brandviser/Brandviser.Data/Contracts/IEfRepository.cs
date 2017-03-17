using System.Collections.Generic;
using System.Linq;

namespace Brandviser.Data.Contracts
{
    public interface IEfRepository<T> where T : class
    {
        IQueryable<T> All { get; }

        IEnumerable<T> GetAll();

        T GetById(int id);

        T GetByGuidId(string id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
