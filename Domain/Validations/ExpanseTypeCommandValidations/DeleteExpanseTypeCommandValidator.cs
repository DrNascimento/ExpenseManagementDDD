using Domain.Commands.ExpanseTypeCommands;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.ExpanseTypeCommandValidations
{
    public class DeleteExpanseTypeCommandValidator : ExpanseTypeCommandValidations<DeleteExpanseTypeCommand>
    {
        public DeleteExpanseTypeCommandValidator(IExpanseTypeRepository expanseTypeRepository) 
            : base(expanseTypeRepository)
        {
            // not implemented
        }
    }
}
