using Domain.Commands.ExpenseInstallmentCommands;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.ExpenseInstallmentCommandHandlers
{
    public class UpdateExpenseInstallmentCommandHandler : UnitOfWorkCommandHandler,
        IRequestHandler<UpdateExpenseInstallmentCommand, Unit>,
        IRequestHandler<TogglePaidExpenseInstallmentCommand, Unit>
    {
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;

        public UpdateExpenseInstallmentCommandHandler(IUnitOfWork unitOfWork,
            IExpenseInstallmentRepository expenseInstallmentRepository) 
            : base(unitOfWork)
        {
            _expenseInstallmentRepository = expenseInstallmentRepository;
        }

        public async Task<Unit> Handle(UpdateExpenseInstallmentCommand request, CancellationToken cancellationToken)
        {
            var installment = await _expenseInstallmentRepository.GetById(request.Id) 
                ?? throw new ResourceNotFoundException("Installment not found");

            installment.DueDate = request.DueDate;
            installment.Amount = request.Amount;
            installment.IsPaid = request.IsPaid;

            _expenseInstallmentRepository.Update(installment);

            await _uow.CommitAsync();

            return Unit.Value;
        }

        public async Task<Unit> Handle(TogglePaidExpenseInstallmentCommand request, CancellationToken cancellationToken)
        {
            var installment = await _expenseInstallmentRepository.GetById(request.Id)
                ?? throw new ResourceNotFoundException("Installment not found");

            installment.IsPaid = !installment.IsPaid;

            _expenseInstallmentRepository.Update(installment);

            await _uow.CommitAsync();

            return Unit.Value;
        }
    }
}
