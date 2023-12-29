using Application.Interfaces;
using Application.ViewModel.ExpenseInstallment;
using AutoMapper;
using Domain.Commands.ExpenseInstallmentCommands;
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
    public class ExpenseInstallmentAppService : IExpenseInstallmentAppService
    {
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;
        private readonly IMediator _mediator;

        public ExpenseInstallmentAppService(IUserContext userContext, 
            IMapper mapper,
            IExpenseInstallmentRepository expenseInstallmentRepository,
            IMediator mediator)
        {
            _userContext = userContext;
            _mapper = mapper;
            _expenseInstallmentRepository = expenseInstallmentRepository;
            _mediator = mediator;
        }

        public async Task<ExpenseInstallmentViewModel> Get(int id)
        {
            var expenseInstallment = await _expenseInstallmentRepository.GetExpenseInstallment(id, _userContext.UserId);
            return _mapper.Map<ExpenseInstallmentViewModel>(expenseInstallment);
        }

        public IEnumerable<ExpenseInstallmentViewModel> GetByDate(int year, int month, int day)
        {
            var expensesInstallments = 
                _expenseInstallmentRepository.GetExpenseInstallments(_userContext.UserId)
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

        public async Task Update(int id, UpdateExpenseInstallmentViewModel updateExpenseInstallmentViewModel)
        {
            updateExpenseInstallmentViewModel.Id = id;
            updateExpenseInstallmentViewModel.UserId = _userContext.UserId;
            var command = _mapper.Map<UpdateExpenseInstallmentCommand>(updateExpenseInstallmentViewModel);

            await _mediator.Send(command);
        }

        public async Task UpdatePaid(int id)
        {
            var command = new TogglePaidExpenseInstallmentCommand(id, _userContext.UserId);
            await _mediator.Send(command);
        }

        public async Task Delete(int id)
        {
            var command = new DeleteExpenseInstallmentCommand(id, _userContext.UserId);
            await _mediator.Send(command);
        }
    }
}
