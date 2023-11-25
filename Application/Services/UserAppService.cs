using Application.Interfaces;
using Application.ViewModel;
using Domain.Interfaces.Repository;
using AutoMapper;
using Application.ViewModel.User;
using Domain.Commands.UserCommands;
using MediatR;

namespace Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private bool disposedValue;

        public UserAppService(IUserRepository userRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<UserViewModel> GetById(int id)
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

        public async Task Update(int id, UpdateUserViewModel updateUserViewModel)
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
}
