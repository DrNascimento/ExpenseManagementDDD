using Domain.CommandHandler;
using Domain.Commands.UserCommands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using Domain.Notifications.UserNotifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.UserCommandHandlers
{
    public class CreateUserCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<CreateUserCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUnitOfWork uow,
            IMediator mediator,
            IUserRepository userRepository)
            : base(uow)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = command.Name,
                Email = command.Email,
                Password = command.Password
            };

            _userRepository.Add(user);

            await _uow.CommitAsync();

            await _mediator.Publish(new CreatedUserNotification { Id = user.Id, Email = user.Email, Name = user.Name, Password = user.Password });

            return user.Id;
        }
    }
}
