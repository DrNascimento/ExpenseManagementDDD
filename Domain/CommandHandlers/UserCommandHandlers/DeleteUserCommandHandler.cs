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
    public class DeleteUserCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<DeleteUserCommand>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUnitOfWork uow,
            IMediator mediator,
            IUserRepository userRepository)
            : base(uow)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id) 
                ?? throw new InvalidOperationException("user not found");

            _userRepository.Delete(user);
            await _uow.CommitAsync();
            await _mediator.Publish("");
        }
    }
}
