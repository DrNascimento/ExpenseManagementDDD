using Domain.Commands.UserCommands;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.UserCommandValidations
{
    public class UpdateUserCommandValidator : UserCommandValidations<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(IUserRepository userRepository) : base(userRepository)
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
        }
    }
}
