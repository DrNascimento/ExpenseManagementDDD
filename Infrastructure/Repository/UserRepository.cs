using Domain.Interfaces.Repository;
using Entities.Entities;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected new readonly ExpanseManagementContext Db;
        protected new readonly DbSet<User> DbSet;


        public UserRepository(ExpanseManagementContext context) 
            : base(context) 
        {
            Db = context;
            DbSet = Db.Set<User>();
        } 

    }
}
