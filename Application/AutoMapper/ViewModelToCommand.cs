using Application.ViewModel.Account;
using AutoMapper;
using Domain.Commands.UserCommands;
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
            CreateMap<CreateNewAccountViewModel, CreateUserCommand>().
                ConstructUsing(c => new CreateUserCommand(c.Name, c.Email, c.Password));
        }
    }
}
