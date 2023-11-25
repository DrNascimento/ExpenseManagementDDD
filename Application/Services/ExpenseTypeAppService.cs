using Application.Interfaces;
using Application.ViewModel.ExpenseType;
using AutoMapper;
using Domain.Commands.ExpenseTypeCommands;
using Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExpenseTypeAppService : IExpenseTypeAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private bool disposedValue;

        public ExpenseTypeAppService(IMapper mapper,
            IMediator mediator,
            IExpenseTypeRepository expenseTypeRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _expenseTypeRepository = expenseTypeRepository;
        }

        public async Task<ExpenseTypeViewModel> GetById(int id)
        {
            var expense = await _expenseTypeRepository.GetById(id);
            return _mapper.Map<ExpenseTypeViewModel>(expense);
        }

        public IEnumerable<ExpenseTypeViewModel> GetAll()
        {
            var expenses = _expenseTypeRepository.GetAll();

            return _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenses);
        }

        public async Task<int> Create(ExpenseTypeViewModel expenseTypeViewModel)
        {
            var command = _mapper.Map<CreateExpenseTypeCommand>(expenseTypeViewModel);

            return await _mediator.Send(command);
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
