using Application.ViewModel.Account;
using Application.ViewModel.ExpanseType;
using AutoMapper;
using Domain.Commands.ExpanseTypeCommands;
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

            CreateMap<ExpanseTypeViewModel, CreateExpanseTypeCommand>()
                .ConstructUsing(e => new CreateExpanseTypeCommand(e.Name, e.IsFixed));



        }
    }
}
