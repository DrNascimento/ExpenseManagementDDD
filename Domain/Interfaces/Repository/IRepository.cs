using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IRepository <TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        Task Add(List<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void HardDelete(TEntity entity);

        int SaveChanges();

        Task<TEntity> GetById(Guid id);

        Task<TEntity> GetByIdIncludingDeleted(Guid id);

        IQueryable<TEntity> GetAll();
    }
}
