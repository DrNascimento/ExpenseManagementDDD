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
using System.Text.RegularExpressions;
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
            ValidateBeforeLogIn(loginViewModel);
            loginViewModel = OnBeforeLoginApply(loginViewModel);

            var user = await _userRepository.GetByEmail(loginViewModel.Email);

            ValidateAfterLogin(user, loginViewModel.Password);

            return _tokenAppService.GenerateToken(user);         
        }

        public async Task<int> Create(CreateNewAccountViewModel createNewAccountViewModel)
        {
            ValidateBeforeCreate(createNewAccountViewModel);
            createNewAccountViewModel = ApplyBeforeCreate(createNewAccountViewModel);

            var command = _mapper.Map<CreateUserCommand>(createNewAccountViewModel);

            return await _mediator.Send(command);
        }



        #region Validations
        private void ValidateBeforeLogIn(LoginViewModel loginViewModel)
        {
            if (loginViewModel == null) 
                throw 
                    new ArgumentNullException(nameof(loginViewModel));

            if (string.IsNullOrWhiteSpace(loginViewModel.Email))
                throw new 
                    ApplicationException($"{nameof(loginViewModel.Email)} cannot be empty");

            if (string.IsNullOrEmpty(loginViewModel.Password))
                throw new
                    ApplicationException($"{nameof(loginViewModel.Password)} cannot be empty");
        }



        private LoginViewModel OnBeforeLoginApply(LoginViewModel loginViewModel)
        {
            loginViewModel.Email = loginViewModel.Email.Trim();
            loginViewModel.Password = loginViewModel.Password;

            return loginViewModel;
        }

        private void ValidateAfterLogin (User user, string unhashedPassword)
        {
            if (user is null)
                throw 
                    new ApplicationException("Email or password is invalid");

            if (new BCryptHash().VerifyPassword(unhashedPassword, user.Password) == false)
                throw
                    new ApplicationException("Email or password is invalid");

        }
        #endregion

        private void ValidateBeforeCreate(CreateNewAccountViewModel createNewAccount)
        {
            if (createNewAccount == null)
                throw new ApplicationException("Email and password is required");

            if (createNewAccount.ConfirmPassword != createNewAccount.Password)
                throw new ApplicationException("The passwords does not match");
        }

        private CreateNewAccountViewModel ApplyBeforeCreate(CreateNewAccountViewModel createNewAccount)
        {
            createNewAccount.Email = createNewAccount.Email.Trim();
            createNewAccount.Name = createNewAccount.Name.Trim();

            return createNewAccount;
        }
    }
}
