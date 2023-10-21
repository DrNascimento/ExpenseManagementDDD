using Domain.Entities;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context
{
    public partial class ExpanseManagementContext : DbContext
    {
        public ExpanseManagementContext() { }

        public ExpanseManagementContext(DbContextOptions<ExpanseManagementContext> options) : base(options)
        { }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ExpanseType> ExpanseTypes { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserExpanse> UserExpanses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseSqlite(@"Data Source=C:\projetos\ExpenseManagementDDD\ExpenseManagementDDD\Infrastructure.SQLiteDatabase\expanse_management.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enum --> TypeUserEnum
            modelBuilder.Entity<User>()
                .Property(u => u.UserTypeEnum)
                .HasConversion(
                    v => v.ToString(), 
                    v => (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), v) 
                );
        }
    }
}
