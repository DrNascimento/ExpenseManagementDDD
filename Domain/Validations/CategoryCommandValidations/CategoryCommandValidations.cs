using Domain.Commands.Category;
using Domain.Interfaces.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.CategoryCommandValidations
{
    public class CategoryCommandValidations<T> : AbstractValidator<T> where T : CategoryCommand
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommandValidations(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        protected void ValidateId() =>
            RuleFor(c => c.Id)
                .NotEmpty();

        protected void ValidateName() =>
            RuleFor(c => c.Name)
                .NotEmpty();

        protected void ValidateNameAlreadyExists() =>
            RuleFor(c => c)
                .Must(c => _categoryRepository.IsNameAvailable(c.Name, c.UserId, c.Id))
                .WithName("Name")
                .WithMessage("This name is already in use");

        protected void ValidateUse()
        {
            //TODO: IMPLEMENT THE VALIDATION USE IN expense
        }
            
    }
}
