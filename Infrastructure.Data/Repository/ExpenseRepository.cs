﻿using Domain.Entities;
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
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ExpenseManagementContext context) : base(context)
        {
        }

        public async Task<Expense> GetExpenseById(int id)
        {
            return await GetIncludes()
                .FirstOrDefaultAsync(x => 
                    x.Id == id && !x.IsDeleted);
        }

        public IEnumerable<Expense> GetExpenses(int userId)
        {
            return GetIncludes()
                .AsNoTracking()
                .Where(e => 
                    !e.IsDeleted && e.UserId == userId);
        }

        public IQueryable<Expense> GetExpenses() =>
            GetIncludes();

        /// <summary>
        /// Validates if exists a expense by id and userId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool HasExpenseByIdAndUser(int id, int userId)
        {
            return DbSet.Any(e => 
                !e.IsDeleted
                && e.UserId == userId
                && e.Id == id);
        }


        private IQueryable<Expense> GetIncludes() =>
            DbSet
                .Include(x => x.Category)
                .Include(x => x.ExpenseType)
                .Include(x => x.ExpenseInstallments);
    }
}
