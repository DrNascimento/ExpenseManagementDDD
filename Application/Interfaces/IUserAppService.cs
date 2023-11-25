using Application.ViewModel;
using Application.ViewModel.User;

namespace Application.Interfaces
{
    public interface IUserAppService : IDisposable
    {

        Task<UserViewModel> GetById(int id);

        IEnumerable<UserViewModel> GetAll();

        Task Update(int id, UpdateUserViewModel updateUserViewModel);
    }
}
