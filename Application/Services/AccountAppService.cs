using Application.Exceptions;
using Application.Interfaces;
using Application.RequestValidation.Account;
using Application.ViewModel.Account;
using AutoMapper;
using Domain.Commands.UserCommands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using FluentValidation;
using Infrastructure.CrossCutting.Identity;
using MediatR;

namespace Application.Services;

public class AccountAppService(IUserRepository userRepository,
    IMapper mapper,
    IMediator mediator,
    ITokenAppService tokenAppService) : IAccountAppService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly ITokenAppService _tokenAppService = tokenAppService;
    private bool disposedValue;

    public async Task<string> LogIn(LoginViewModel loginViewModel) 
    {
        new LoginValidation().ValidateAndThrow(loginViewModel);

        User user = await _userRepository.GetByEmail(loginViewModel.Email);

        if (user is null || !BCryptHash.VerifyPassword(loginViewModel.Password, user.Password))
            throw new LoginFailedException();

        return _tokenAppService.GenerateToken(user);         
    }

    public async Task<Guid> Create(CreateNewAccountViewModel createNewAccountViewModel)
    {
        var command = _mapper.Map<CreateUserCommand>(createNewAccountViewModel);

        return await _mediator.Send(command);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
