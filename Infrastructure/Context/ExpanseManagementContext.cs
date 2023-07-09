using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context
{
    public class ExpanseManagementContext : DbContext
    {
        public ExpanseManagementContext(DbContextOptions<ExpanseManagementContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
