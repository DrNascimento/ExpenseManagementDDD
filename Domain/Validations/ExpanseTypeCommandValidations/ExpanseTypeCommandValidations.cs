
using Domain.Commands.ExpanseTypeCommands;
using Domain.Interfaces.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.ExpanseTypeCommandValidations
{
    public class ExpanseTypeCommandValidations<T> :
        AbstractValidator<T>
        where T : ExpanseTypeCommand
    {
        private readonly IExpanseTypeRepository _expanseTypeRepository;

        public ExpanseTypeCommandValidations(IExpanseTypeRepository expanseTypeRepository) 
        {
            _expanseTypeRepository = expanseTypeRepository;
        }

        protected void ValidateName() =>
            RuleFor(x => x.Name)
                .NotEmpty();

        protected void ValidateNameAlreadyExists() => 
            RuleFor(x => x)
                .Must(x => _expanseTypeRepository.IsNameAvailable(x.Name, x.Id))
                .WithName("Name")
                .WithMessage("This name is already in use");
    }
}
