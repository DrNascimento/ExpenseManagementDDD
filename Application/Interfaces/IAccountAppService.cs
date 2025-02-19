using Application.ViewModel.Account;

namespace Application.Interfaces;

public interface IAccountAppService : IDisposable
{
    Task<Guid> Create(CreateNewAccountViewModel createNewAccountViewModel);

    Task<string> LogIn(LoginViewModel loginViewModel);
}