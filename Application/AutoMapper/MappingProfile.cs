using Application.DTO;
using Application.ViewModel;
using Application.ViewModel.Account;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, LoginResponseViewModel>();

            CreateMap<User, UserViewModel>();
        }
    }
}
