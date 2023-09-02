using Application.Helper;
using Application.Interfaces;
using Application.ViewModel.Account;
using AutoMapper;
using Domain.Commands.UserCommands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Identity;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class AccountAppService : IAccountAppService
    {
       
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITokenAppService _tokenAppService;

        public AccountAppService(IUserRepository userRepository,
            IMapper mapper,
            IMediator mediator,
            ITokenAppService tokenAppService) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mediator = mediator;
            _tokenAppService = tokenAppService;
        }

        public async Task<string> LogIn(LoginViewModel loginViewModel) 
        {
            loginViewModel = OnBeforeLogin(loginViewModel);

            var user = await _userRepository.GetByEmailAndPassword(loginViewModel.Email, loginViewModel.Password);

            VerifyLogin(user);

            return _tokenAppService.GenerateToken(user);         
        }

        public async Task<int> Create(CreateNewAccountViewModel newAccountViewModel)
        {
            newAccountViewModel = OnBeforeRegister(newAccountViewModel);

            //VerifyRegister(newAccountViewModel);

            var command = _mapper.Map<CreateUserCommand>(newAccountViewModel);

            return await _mediator.Send(command);
        }


        private LoginViewModel OnBeforeLogin(LoginViewModel loginViewModel)
        {
            loginViewModel.Email = loginViewModel.Email.Trim();
            loginViewModel.Password = Crypt.StringToSha256(loginViewModel.Password);

            return loginViewModel;
        }

        private void VerifyLogin (User user)
        {
            if (user is null)
                throw new Exception("Email or password is invalid");
        }

        private CreateNewAccountViewModel OnBeforeRegister(CreateNewAccountViewModel newAccountViewModel)
        {
            newAccountViewModel.Email = newAccountViewModel.Email.Trim();
            newAccountViewModel.Name = newAccountViewModel.Name.Trim();
            newAccountViewModel.Password = Crypt.StringToSha256(newAccountViewModel.Password);
            newAccountViewModel.ConfirmPassword = Crypt.StringToSha256(newAccountViewModel.ConfirmPassword);

            return newAccountViewModel;
        }

        private void VerifyRegister(CreateNewAccountViewModel newAccountViewModel)
        { 
            if (!_userRepository.IsEmailAvailable(newAccountViewModel.Email))
                throw new Exception("Email is not available");
        }
    }
}
