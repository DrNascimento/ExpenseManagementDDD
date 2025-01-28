using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly ExpenseManagementContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ExpenseManagementContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public void HardDelete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            entity.Updated = DateTime.UtcNow;
            Db.Update(entity);
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public virtual async Task<TEntity> GetByIdIncludingDeleted(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().Where(x => !x.IsDeleted);
        }
    }
  
