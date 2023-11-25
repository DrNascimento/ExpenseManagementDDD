using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.UserCommands
{
    public class UpdateUserCommand : UserCommand, IRequest<Unit>
    {
        public UpdateUserCommand(int id, string name, string email, UserTypeEnum userTypeEnum) 
        {
            Id = id;
            Name = name;
            Email = email;
            UserTypeEnum = userTypeEnum;
        }
    }
}
