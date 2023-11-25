using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Category
{
    public class DeleteCategoryCommand : CategoryCommand, IRequest<Unit>
    {
        public DeleteCategoryCommand(int id) 
        {
            Id = id;
        }
    }
}
