using Domain.Commands.ExpanseTypeCommands;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.ExpanseTypeCommandHandlers
{
    public class DeleteExpanseTypeCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<DeleteExpanseTypeCommand>
    {
        private readonly IExpanseTypeRepository _expanseTypeRepository;

        public DeleteExpanseTypeCommandHandler(IUnitOfWork unitOfWork,
            IExpanseTypeRepository expanseTypeRepository)
            : base(unitOfWork)
        {
            _uow = unitOfWork;
            _expanseTypeRepository = expanseTypeRepository;
        }

        public async Task Handle(DeleteExpanseTypeCommand request, CancellationToken cancellationToken)
        {
            var expanseType = await _expanseTypeRepository.GetById(request.Id)
                ?? throw new InvalidOperationException("Expanse Type not found.");

            _expanseTypeRepository.Delete(expanseType);

            await _uow.CommitAsync();
        }
    }
}