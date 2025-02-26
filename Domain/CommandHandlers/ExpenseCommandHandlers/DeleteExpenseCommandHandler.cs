using Domain.Commands.ExpenseCommands;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.ExpenseCommandHandlers
{
    public class DeleteExpenseCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<DeleteExpenseCommand, Unit>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;
        public DeleteExpenseCommandHandler(IUnitOfWork unitOfWork,
            IExpenseRepository expenseRepository, 
            IExpenseInstallmentRepository expenseInstallmentRepository) : base(unitOfWork)
        {
            _expenseRepository = expenseRepository;
            _expenseInstallmentRepository = expenseInstallmentRepository;
        }

        public async Task<Unit> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetById(request.Id) 
                ?? throw new ResourceNotFoundException("Expense not found.");

            try
            {
                await _uow.BeginTransactionAsync(cancellationToken);

                _expenseRepository.Delete(expense);
                // delete all expanse installments by expenseId
                await _expenseInstallmentRepository.DeleteByExpenseId(request.Id);
            
                await _uow.CommitTransactionAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception)
            {
                await _uow.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
