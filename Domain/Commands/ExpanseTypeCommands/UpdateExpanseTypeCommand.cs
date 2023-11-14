using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpanseTypeCommands
{
    public class UpdateExpanseTypeCommand : ExpanseTypeCommand, IRequest
    {
        public UpdateExpanseTypeCommand(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
