using Domain.Commands.Category;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.CategoryCommandValidations
{
    public class UpdateCategoryCommandValidator : CategoryCommandValidations<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
            ValidateId();
            ValidateName();
            ValidateNameAlreadyExists();
        }
    }
}
