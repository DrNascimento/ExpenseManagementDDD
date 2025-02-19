using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Category
{
    public class CreateCategoryCommand : CategoryCommand, IRequest<Guid>
    {
        public CreateCategoryCommand(string name, Guid? userId) 
        {
            Name = name;
            UserId = userId;
        }
    }
}
