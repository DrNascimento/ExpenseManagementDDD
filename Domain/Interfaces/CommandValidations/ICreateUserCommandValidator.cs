using Domain.Commands.UserCommands;
using Domain.Validations.UserCommandValidations;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.CommandValidations
{
    public interface ICreateUserCommandValidator
    {
        ValidationResult Validate(CreateUserCommand command);
    }
}
