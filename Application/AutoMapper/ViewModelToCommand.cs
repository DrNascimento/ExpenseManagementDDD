using Application.ViewModel.Account;
using Application.ViewModel.Category;
using Application.ViewModel.Expense;
using Application.ViewModel.ExpenseType;
using Application.ViewModel.User;
using AutoMapper;
using Domain.Commands.Category;
using Domain.Commands.ExpenseCommands;
using Domain.Commands.ExpenseTypeCommands;
using Domain.Commands.UserCommands;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class ViewModelToCommand : Profile
    {
        public ViewModelToCommand() 
        {
            CreateMap<CreateNewAccountViewModel, CreateUserCommand>()
                .ConstructUsing(c => new CreateUserCommand(c.Name, c.Email, c.Password, (UserTypeEnum)c.UserTypeEnum));

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
                    e.Installments, e.ExpenseInstallmentAmmount, e.ExpenseInstallmentDueDate));

            CreateMap<UpdateExpenseCommand, UpdateExpenseCommand>()
                .ConstructUsing(e => new UpdateExpenseCommand(e.Id, e.Name, e.CategoryId));

            

        }
    }
}
