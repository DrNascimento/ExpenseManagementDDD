using Domain.Commands.ExpenseInstallmentCommands;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.ExpenseInstallmentCommandHandlers
{
    public class DeleteExpenseInstallmentCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<DeleteExpenseInstallmentCommand, Unit>
    {
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;

        public DeleteExpenseInstallmentCommandHandler(IUnitOfWork unitOfWork,
            IExpenseInstallmentRepository expenseInstallmentRepository) : base(unitOfWork)
        {
            _expenseInstallmentRepository = expenseInstallmentRepository;
        }

        public async Task<Unit> Handle(DeleteExpenseInstallmentCommand request, CancellationToken cancellationToken)
        {
            var installment = await _expenseInstallmentRepository.GetById(request.Id)
                ?? throw new ResourceNotFoundException("Installment not found");

            _expenseInstallmentRepository.Delete(installment);

            await _uow.CommitAsync();

            return Unit.Value;
        }
    }
}
