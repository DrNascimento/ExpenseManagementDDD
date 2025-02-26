using Application.Interfaces;
using Application.ViewModel;
using Domain.Interfaces.Repository;
using AutoMapper;
using Application.ViewModel.User;
using Domain.Commands.UserCommands;
using MediatR;

namespace Application.Services;

public class UserAppService(IUserRepository userRepository,
    IMapper mapper,
    IMediator mediator) : IUserAppService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private bool disposedValue;

    public async Task<UserViewModel> GetById(Guid id)
    {
        var user = await _userRepository.GetById(id);

        var userViewModel = _mapper.Map<UserViewModel>(user);

        return userViewModel;
    }

    public IEnumerable<UserViewModel> GetAll()
    {
        var users = _userRepository.GetAll();

        var usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

        return usersViewModel;
    }

    public async Task Update(Guid id, UpdateUserViewModel updateUserViewModel)
    {
        updateUserViewModel.Id = id;
        var command = _mapper.Map<UpdateUserCommand>(updateUserViewModel);
        await _mediator.Send(command);
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
