using Domain.Commands.UserCommands;
using Domain.Interfaces.CommandValidations;
using Domain.Interfaces.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.UserCommandValidations
{
    public abstract class UserCommandValidations<T> :
        AbstractValidator<T> 
        where T : UserCommand
    {
        private readonly IUserRepository _userRepository;

        public UserCommandValidations(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public void ValidateId() =>
            RuleFor(x => x.Id)
                .NotEmpty();

        public void ValidateName() => 
            RuleFor(x => x.Name) 
                .NotEmpty()
                .Length(1, 100);

        public void ValidateEmail() =>
            RuleFor(x => x.Email)
                .NotEmpty()
                .Length(1, 100)
                .EmailAddress();

        public void ValidateEmailAvailability() =>
            RuleFor(x => x.Email)
                .Must(_userRepository.IsEmailAvailable)
                    .WithMessage("Email is not available");

        public void ValidatePassword() =>
            RuleFor(x => x.Password)
                .NotEmpty();

        public void ValidateExisting() =>
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(_userRepository.HasUserById)
                .WithMessage("User does not exist");
            
    }
}
