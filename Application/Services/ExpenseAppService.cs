using Application.Interfaces;
using Application.ViewModel.Expense;
using AutoMapper;
using Domain.Commands.ExpenseCommands;
using Domain.Interfaces.Repository;
using Infrastructure.CrossCutting.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExpenseAppService : IExpenseAppService
    {
        private bool disposedValue;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public ExpenseAppService(IExpenseRepository expenseRepository, 
            IMediator mediator, 
            IMapper mapper,
            IUserContext userContext) 
        {
            _expenseRepository = expenseRepository;
            _mediator = mediator;
            _mapper = mapper;
            _userContext = userContext;
        }

        public ExpenseViewModel Get(int id)
        {
            var expense = _expenseRepository
                .GetExpenses(Convert.ToInt16(_userContext.UserId))
                .FirstOrDefault(e => e.Id == id);

            return _mapper.Map<ExpenseViewModel>(expense);
        }

        public IEnumerable<ExpenseViewModel> GetAll()
        {
            var expenses = _expenseRepository.GetExpenses(Convert.ToInt16(_userContext.UserId));
            return _mapper.Map<IEnumerable<ExpenseViewModel>>(expenses);
        }

        public IEnumerable<ExpenseViewModel> GetByDate(int year, int month, int day)
        {
            var expenses = _expenseRepository.GetExpenses();

            expenses.Where(e =>
                e.ExpenseInstallments
                    .Any(ei => ei.DueDate.Year == year
                        && (month < 1 || ei.DueDate.Month == month)
                        && (day < 1 || ei.DueDate.Day == day)
                        ));

            return _mapper.Map<IEnumerable<ExpenseViewModel>>(expenses);
        }

        public async Task<int> Create(CreateExpenseViewModel createExpenseViewModel)
        {
            createExpenseViewModel.UserId = Convert.ToInt16(_userContext.UserId);
            var command = _mapper.Map<CreateExpenseCommand>(createExpenseViewModel);
            return await _mediator.Send(command);
        }

        public async Task Update(int id, UpdateExpenseViewModel updateExpenseViewModel)
        {
            await ValidatePermission(id);

            var command = _mapper.Map<UpdateExpenseCommand>(updateExpenseViewModel);
            await _mediator.Send(command);
        }

        public async Task Delete(int id)
        {
            await ValidatePermission(id);
            await _mediator.Send(new DeleteExpenseCommand(id));   
        }

        public async Task ValidatePermission(int id)
        {
            var expense = await _expenseRepository.GetById(id)
                ?? throw new InvalidOperationException("Expense not found");

            if (Convert.ToInt16(_userContext.UserId) != expense.UserId)
                throw new InvalidOperationException("You don't have permission");

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
