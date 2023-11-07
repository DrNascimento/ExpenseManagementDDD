using Application.ViewModel;

namespace Application.Interfaces
{
    public interface IUserAppService : IDisposable
    {

        Task<UserViewModel> GetById(int id);

        IEnumerable<UserViewModel> GetAll();
    }
}
