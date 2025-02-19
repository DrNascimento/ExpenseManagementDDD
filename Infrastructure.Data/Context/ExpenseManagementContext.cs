using Domain.Entities;
using Domain.Enums;
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
    public partial class ExpenseManagementContext : DbContext
    {
        public ExpenseManagementContext() { }

        public ExpenseManagementContext(DbContextOptions<ExpenseManagementContext> options) : base(options)
        { }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Expense> Expenses { get; set; }

        public virtual DbSet<ExpenseInstallment> ExpenseInstallments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseSqlite(@"Data Source=C:\projetos\ExpenseManagementDDD\ExpenseManagementDDD\Infrastructure.SQLiteDatabase\expense_management.db");
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
