using Domain.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Infrastructure.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected new readonly ExpenseManagementContext Db;
        protected new readonly DbSet<User> DbSet;


        public UserRepository(ExpenseManagementContext context) 
            : base(context) 
        {
            Db = context;
            DbSet = Db.Set<User>();
        } 

        public async Task<User> GetByEmailAndPassword (string email, string password)
        {
            return await DbSet
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password && !u.IsDeleted);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await DbSet
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }


        public bool IsEmailAvailable(string email)
        {
            return !DbSet.Any(u => u.Email == email && !u.IsDeleted);
        }

        public bool HasUserById(int id)
        {
            return DbSet.Any(u => u.Id == id);
        }
    }
}
