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

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }


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

            modelBuilder.Entity<User>()
                .HasMany(u => u.RefreshTokens)
                .WithOne()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .Navigation(u => u.RefreshTokens)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<RefreshToken>(token =>
            {
                token.ToTable("RefreshTokens");
                token.HasKey(t => t.Id);
                token.Property(t => t.Id).ValueGeneratedNever();
                token.Property(t => t.TokenHash).IsRequired().HasMaxLength(64);
                token.HasIndex(t => t.TokenHash).IsUnique();
                token.HasIndex(t => t.UserId);
                token.Ignore(t => t.IsRevoked);
            });
        }
    }
}
