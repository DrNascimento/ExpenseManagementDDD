using Domain.Interfaces.Repository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ExpanseManagementContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ExpanseManagementContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            SetCreatedField(entity, DateTime.UtcNow);
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            SetIsDeleteField(entity, true);
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
            SetUpdatedField(entity, DateTime.UtcNow);
            Db.Update(entity);
        }

        public virtual async Task<TEntity> GetById(int iid)
        {
            return await DbSet.FindAsync(iid);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        private void SetCreatedField(TEntity entity, DateTime createdAt)
        {
            entity.GetType().GetProperty("Created")?.SetValue(entity, createdAt);
        }

        private void SetIsDeleteField(TEntity entity, bool isDelete)
        {
            entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, isDelete);
        }

        private void SetUpdatedField(TEntity entity, DateTime updatedAt)
        {
            entity.GetType().GetProperty("Created")?.SetValue(entity, updatedAt);
        }
    }
  }
