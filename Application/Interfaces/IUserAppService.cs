using Application.ViewModel;

namespace Application.Interfaces
{
    public interface IUserAppService
    {

        Task<UserViewModel> GetById(int id);

        IEnumerable<UserViewModel> GetAll();
    }
}
