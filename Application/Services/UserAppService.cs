using Application.Interfaces;
using Application.ViewModel;
using Domain.Interfaces.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserAppService(IUserRepository userRepository,
            IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserViewModel> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            var userViewModel  = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        public async Task<IList<UserViewModel>> GetAll()
        {
            var users = _userRepository.GetAll();

            var usersViewModel = users.Select(x => new UserViewModel { Id = x.Id, Email = x.Email, Name = x.Name }).ToList();


            return usersViewModel;

            
        }

        
    }
}
