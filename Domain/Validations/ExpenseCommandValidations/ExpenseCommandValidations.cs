﻿using Domain.Commands.ExpenseCommands;
using FluentValidation;

namespace Domain.Validations.ExpenseCommandValidations;

public class ExpenseCommandValidations<T> : AbstractValidator<T> where T : ExpenseCommand
{
    public ExpenseCommandValidations()
    {

    }
    public void ValidateId() =>
        RuleFor(e => e.Id)
            .NotEmpty();

    public void ValidateUserId() =>
        RuleFor(e => e.UserId)
            .NotEmpty();

    public void ValidateName() =>
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(32);

    public void ValidateExpenseTypeId() =>
        RuleFor(e => e.ExpenseTypeId)
            .NotEmpty();

    public void ValidateCategoryId() =>
        RuleFor(e => e.CategoryId)
            .NotEmpty();

    public void ValidateInstallments() =>
        RuleFor(e => e.Installments)
            .InclusiveBetween(1, 24);

    public void ValidateAmount() =>
        RuleFor(e => e.ExpenseInstallmentAmmount)
            .NotEmpty();

    public void ValidateDueDate() =>
        RuleFor(e => e.ExpenseInstallmentDueDate)
            .InclusiveBetween(DateTime.UtcNow.Date, DateTime.MaxValue)
            .WithMessage("The Due Date must be greater than or equal to today");

}
