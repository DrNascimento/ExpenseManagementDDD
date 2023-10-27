using Domain.CommandHandler;
using Domain.Commands.ExpanseTypeCommands;
using Domain.Commands.UserCommands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.ExpanseTypeCommandHandlers
{
    public class CreateExpanseTypeCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<CreateExpanseTypeCommand, int>
    {
        private readonly IExpanseTypeRepository _expanseTypeRepository;

        public CreateExpanseTypeCommandHandler(IUnitOfWork unitOfWork,
            IExpanseTypeRepository expanseTypeRepository) 
            : base(unitOfWork)
        {
            _uow = unitOfWork;
            _expanseTypeRepository = expanseTypeRepository;
        }

        public async Task<int> Handle(CreateExpanseTypeCommand request, CancellationToken cancellationToken)
        {
            var expanseType = new ExpanseType
            {
                Name = request.Name,
                IsFixed = request.IsFixed
            };

            _expanseTypeRepository.Add(expanseType);

            await _uow.CommitAsync();

            return expanseType.Id;
        }
    }
}
