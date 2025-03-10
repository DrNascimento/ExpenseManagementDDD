﻿using Application.ViewModel.Account;
using Application.ViewModel.Category;
using Application.ViewModel.Expense;
using Application.ViewModel.ExpenseInstallment;
using Application.ViewModel.ExpenseType;
using Application.ViewModel.User;
using AutoMapper;
using Domain.Commands.Category;
using Domain.Commands.ExpenseCommands;
using Domain.Commands.ExpenseInstallmentCommands;
using Domain.Commands.ExpenseTypeCommands;
using Domain.Commands.UserCommands;
using Domain.Enums;

namespace Application.AutoMapper;

public class ViewModelToCommand : Profile
{
    public ViewModelToCommand() 
    {
        CreateMap<CreateNewAccountViewModel, CreateUserCommand>()
            .ConstructUsing(c => new CreateUserCommand(c.Name, c.Email, c.Password, c.ConfirmPassword, (UserTypeEnum)c.UserTypeEnum));

        CreateMap<UpdateUserViewModel, UpdateUserCommand>()
            .ConstructUsing(u => new UpdateUserCommand(u.Id, u.Name, u.Email, (UserTypeEnum)u.UserTypeEnum));

        CreateMap<ExpenseTypeViewModel, CreateExpenseTypeCommand>()
            .ConstructUsing(e => new CreateExpenseTypeCommand(e.Name, e.IsFixed));

        CreateMap<CreateCategoryViewModel, CreateCategoryCommand>()
            .ConstructUsing(c => new CreateCategoryCommand(c.Name, c.UserId));

        CreateMap<UpdateCategoryViewModel, UpdateCategoryCommand>()
            .ConstructUsing(c => new UpdateCategoryCommand(c.Id, c.Name));

        CreateMap<CreateExpenseViewModel, CreateExpenseCommand>()
            .ConstructUsing(e => new CreateExpenseCommand(e.UserId, e.Name, e.ExpenseTypeId, e.CategoryId,
                e.Installments, e.ExpenseInstallmentAmount, e.ExpenseInstallmentDueDate));

        CreateMap<UpdateExpenseViewModel, UpdateExpenseCommand>()
            .ConstructUsing(e => new UpdateExpenseCommand(e.Id, e.Name, e.CategoryId));

        CreateMap<UpdateExpenseInstallmentViewModel, UpdateExpenseInstallmentCommand>()
            .ConstructUsing(e => new UpdateExpenseInstallmentCommand(e.Id, e.Amount, e.DueDate, e.IsPaid, e.UserId));

    }
}
