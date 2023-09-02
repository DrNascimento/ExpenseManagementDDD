using Domain.Commands.UserCommands;
using Domain.Interfaces.CommandValidations;
using Domain.Interfaces.Repository;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.UserCommandValidations
{
    public class CreateUserCommandValidator : UserCommandValidations<CreateUserCommand>
            
    {
        public CreateUserCommandValidator(IUserRepository userRepository)
            : base(userRepository)
        {
            ValidateName();
            ValidateEmail();
            ValidateEmailAvailability();
            ValidatePassword();

            // Unique
            ValidateConfirmPassword();
            ValidatePasswordLength();
        }

        private void ValidateConfirmPassword() => 
            RuleFor(u => u.ConfirmPassword)
                .Equal(u1 => u1.Password);

        public void ValidatePasswordLength() =>
            RuleFor(x => x.PasswordLength)
                .LessThanOrEqualTo(64);
    }
}
