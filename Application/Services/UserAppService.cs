using Application.Interfaces;
using Application.ViewModel;
using Domain.Interfaces.Repository;
using AutoMapper;

namespace Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private bool disposedValue;

        public UserAppService(IUserRepository userRepository,
            IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
