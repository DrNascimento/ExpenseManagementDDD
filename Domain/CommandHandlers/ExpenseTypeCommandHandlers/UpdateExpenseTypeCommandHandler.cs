using Domain.Commands.ExpenseTypeCommands;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.ExpenseTypeCommandHandlers
{
    public class UpdateExpenseTypeCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<UpdateExpenseTypeCommand, Unit>
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public UpdateExpenseTypeCommandHandler(IUnitOfWork unitOfWork,
            IExpenseTypeRepository expenseTypeRepository)
            : base(unitOfWork)
        {
            _uow = unitOfWork;
            _expenseTypeRepository = expenseTypeRepository;
        }

        public async Task<Unit> Handle(UpdateExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            var expenseType = await _expenseTypeRepository.GetById(request.Id)
                ?? throw new ResourceNotFoundException("expense Type not found.");

            expenseType.Name = request.Name;
            expenseType.IsFixed = request.IsFixed;

            _expenseTypeRepository.Update(expenseType);

            await _uow.CommitAsync();

            return Unit.Value;
        }
    }
}