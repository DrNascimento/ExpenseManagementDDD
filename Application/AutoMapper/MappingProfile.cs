using Application.ViewModel;
using Application.ViewModel.Account;
using Application.ViewModel.Category;
using Application.ViewModel.Expense;
using Application.ViewModel.ExpenseInstallment;
using Application.ViewModel.ExpenseType;
using AutoMapper;
using Domain.Entities;
using Domain.View;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<User, LoginResponseViewModel>();

        CreateMap<User, UserViewModel>();

        CreateMap<ExpenseType, ExpenseTypeViewModel>();

        CreateMap<Expense, ExpenseViewModel>();

        CreateMap<Expense, ExpenseToInstallment>();

        CreateMap<ExpenseInstallment, InstallmentViewModel>();

        CreateMap<ExpenseInstallment, ExpenseInstallmentViewModel>();            

        CreateMap<Category, CategoryViewModel>();

        CreateMap<SummaryCategoriesView, CategoriesSummaryViewModel>();

        CreateMap<SummaryCategoryView, CategorySummaryViewModel>();
    }
}
