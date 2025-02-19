using Domain.Commands.ExpenseCommands;
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
    public class UpdateExpenseCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<UpdateExpenseCommand, Unit>
    {
        private readonly IExpenseRepository _expenseRepository;

        public UpdateExpenseCommandHandler(IUnitOfWork unitOfWork,
            IExpenseRepository expenseRepository) 
            : base(unitOfWork)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Unit> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetById(request.Id) ??
                throw new InvalidOperationException("Expense not found");

            expense.Name = request.Name;
            expense.CategoryId = request.CategoryId;

            _expenseRepository.Update(expense);

            await _uow.CommitAsync();

            return Unit.Value;
        }
    }
}
