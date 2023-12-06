using Application.Interfaces;
using Application.ViewModel.ExpenseInstallment;
using AutoMapper;
using Domain.Interfaces.Repository;
using Infrastructure.CrossCutting.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExpenseInstallmentAppService : IExpenseInstallmentAppService
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;

        public ExpenseInstallmentAppService(IUserContext userContext, 
            IMapper mapper,
            IExpenseInstallmentRepository expenseInstallmentRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _expenseInstallmentRepository = expenseInstallmentRepository;
        }

        public async Task<ExpenseInstallmentViewModel> Get(int id)
        {
            int userId = Convert.ToInt16(_userContext.UserId);
            var expenseInstallment = await _expenseInstallmentRepository.GetExpenseInstallment(id, userId);
            return _mapper.Map<ExpenseInstallmentViewModel>(expenseInstallment);
        }

        public IEnumerable<ExpenseInstallmentViewModel> GetByDate(int year, int month, int day)
        {
            int userId = Convert.ToInt16(_userContext.UserId);
            var expensesInstallments = 
                _expenseInstallmentRepository.GetExpenseInstallments(userId)
                    .Where(e => 
                        e.DueDate.Year == year
                        && (month == 0 || e.DueDate.Month == month)
                        && (day == 0 || e.DueDate.Day == day)
                    ).OrderBy(e => e.InstallmentNumber);

            return _mapper.Map<IEnumerable<ExpenseInstallmentViewModel>>(expensesInstallments);
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
