using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Category
{
    public class UpdateCategoryCommand : CategoryCommand, IRequest<Unit>
    {
        public UpdateCategoryCommand(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
