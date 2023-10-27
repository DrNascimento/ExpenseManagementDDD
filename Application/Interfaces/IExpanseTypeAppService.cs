using Application.ViewModel.ExpanseType;

namespace Application.Interfaces
{
    public interface IExpanseTypeAppService
    {
        Task<int> Create(ExpanseTypeViewModel expanseTypeViewModel);

        Task<ExpanseTypeViewModel> GetById(int id);

        IEnumerable<ExpanseTypeViewModel> GetAll();
    }
}
