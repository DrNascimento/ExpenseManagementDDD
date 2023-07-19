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
        public CreateUserCommand(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
        }
    }
}
