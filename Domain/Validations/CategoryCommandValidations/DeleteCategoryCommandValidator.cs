using Domain.Commands.Category;
using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations.CategoryCommandValidations
{
    public class DeleteCategoryCommandValidator : CategoryCommandValidations<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator(ICategoryRepository categoryRepository) 
            : base(categoryRepository)
        {
            ValidateId();
            ValidateUse();
        }
    }
}
