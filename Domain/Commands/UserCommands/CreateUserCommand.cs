using Domain.Interfaces.Repository;
using Domain.Validations.UserCommandValidations;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.UserCommands
{
    public class CreateUserCommand : UserCommand, IRequest<int>
    {
        public CreateUserCommand(string name, string email, string password, string confirmPassword, int passwordLength)
        {
            Name = name;
            Password = password;
            Email = email;
            ConfirmPassword = confirmPassword;
            PasswordLength = passwordLength;
        }

        public string ConfirmPassword { get; set; }
        public int PasswordLength { get; set; }
    }
}
