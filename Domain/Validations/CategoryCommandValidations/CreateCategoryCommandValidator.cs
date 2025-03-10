﻿using Domain.Commands.Category;
using Domain.Interfaces.Repository;

namespace Domain.Validations.CategoryCommandValidations;
public class CreateCategoryCommandValidator : CategoryCommandValidations<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository, IExpenseRepository expenseRepository)
        : base(categoryRepository, expenseRepository) 
    {
        ValidateName();
        ValidateNameAlreadyExists();
    }
}
