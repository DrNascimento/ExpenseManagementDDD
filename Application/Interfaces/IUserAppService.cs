using Application.ViewModel;
using Application.ViewModel.User;

namespace Application.Interfaces;

public interface IUserAppService : IDisposable
{

    Task<UserViewModel> GetById(Guid id);

    IEnumerable<UserViewModel> GetAll();

    Task Update(Guid Id, UpdateUserViewModel updateUserViewModel);
}
