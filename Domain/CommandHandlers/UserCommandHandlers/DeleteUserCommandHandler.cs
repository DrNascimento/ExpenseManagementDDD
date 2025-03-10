﻿using Domain.Commands.UserCommands;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.UserCommandHandlers;

public class DeleteUserCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<DeleteUserCommand, Unit>
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

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id) 
            ?? throw new ResourceNotFoundException("User not found");

        _userRepository.Delete(user);
        await _uow.CommitAsync();


        return Unit.Value;
    }
}
