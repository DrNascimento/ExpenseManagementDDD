using Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.UserCommands
{
    public class UpdateUserCommand : UserCommand, IRequest
    {
        public UpdateUserCommand(string name, string email, UserTypeEnum userTypeEnum) 
        {
            Name = name;
            Email = email;
            UserTypeEnum = userTypeEnum;
        }
    }
}
