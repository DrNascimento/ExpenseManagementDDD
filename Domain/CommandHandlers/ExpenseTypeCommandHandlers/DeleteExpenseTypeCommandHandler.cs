using Domain.Commands.ExpenseTypeCommands;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.ExpenseTypeCommandHandlers
{
    public class DeleteExpenseTypeCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<DeleteExpenseTypeCommand, Unit>
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public DeleteExpenseTypeCommandHandler(IUnitOfWork unitOfWork,
            IExpenseTypeRepository expenseTypeRepository)
            : base(unitOfWork)
        {
            _uow = unitOfWork;
            _expenseTypeRepository = expenseTypeRepository;
        }

        public async Task<Unit> Handle(DeleteExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            var expenseType = await _expenseTypeRepository.GetById(request.Id)
                ?? throw new ResourceNotFoundException("expense Type not found.");

            _expenseTypeRepository.Delete(expenseType);

            await _uow.CommitAsync();

            return Unit.Value;
        }
    }
}