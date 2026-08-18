using Application.DTO.Output.Category;
using Application.DTO.Output.Expense;
using Application.DTO.Output.ExpenseInstallment;
using Application.DTO.Output.ExpenseType;
using Application.DTO.Output.User;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserOutput>();

        CreateMap<ExpenseType, ExpenseTypeOutput>();

        CreateMap<Expense, ExpenseOutput>();

        CreateMap<Expense, ExpenseToInstallmentOutput>();

        CreateMap<ExpenseInstallment, InstallmentOutput>();

        CreateMap<ExpenseInstallment, ExpenseInstallmentOutput>();

        CreateMap<Category, CategoryOutput>();

        CreateMap<SummaryCategoriesView, CategoriesSummaryOutput>();

        CreateMap<SummaryCategoryView, CategorySummaryOutput>();
    }
}
