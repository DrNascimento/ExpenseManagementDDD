using Domain.Commands.UserCommands;
using Domain.Interfaces.CommandValidations;
using Domain.Interfaces.Repository;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        }
    }
}
