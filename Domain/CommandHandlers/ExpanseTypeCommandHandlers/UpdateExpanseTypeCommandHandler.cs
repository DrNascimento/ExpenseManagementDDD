using Domain.CommandHandler;
using Domain.Commands.ExpanseTypeCommands;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.ExpanseTypeCommandHandlers
{
    public class UpdateExpanseTypeCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<UpdateExpanseTypeCommand>
    {
        private readonly IExpanseTypeRepository _expanseTypeRepository;

        public UpdateExpanseTypeCommandHandler(IUnitOfWork unitOfWork,
            IExpanseTypeRepository expanseTypeRepository)
            : base(unitOfWork)
        {
            _uow = unitOfWork;
            _expanseTypeRepository = expanseTypeRepository;
        }

        public async Task Handle(UpdateExpanseTypeCommand request, CancellationToken cancellationToken)
        {
            var expanseType = await _expanseTypeRepository.GetById(request.Id)
                ?? throw new InvalidOperationException("Expanse Type not found.");

            expanseType.Name = request.Name;
            expanseType.IsFixed = request.IsFixed;

            _expanseTypeRepository.Update(expanseType);

            await _uow.CommitAsync();
        }
    }
}