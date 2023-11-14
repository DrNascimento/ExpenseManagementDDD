using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.UserCommands
{
    public class DeleteUserCommand : UserCommand, IRequest
    {
        public DeleteUserCommand(int id) 
        {
            Id = id;
        }
    }
}
