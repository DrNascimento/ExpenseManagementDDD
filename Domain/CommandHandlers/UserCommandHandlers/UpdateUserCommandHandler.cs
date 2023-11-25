using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.UserCommandHandlers
{
    public class UpdateUserCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork,
            IUserRepository userRepository) : 
            base(unitOfWork)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(500);
            return Unit.Value;
        }

    }

}
