using Application.Interfaces;
using Application.ViewModel.Account;
using AutoMapper;
using Domain.Commands.UserCommands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.CrossCutting.Identity;
using MediatR;


namespace Application.Services;

public class AccountAppService : IAccountAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITokenAppService _tokenAppService;
    private bool disposedValue;

    public AccountAppService(IUserRepository userRepository,
        IMapper mapper,
        IMediator mediator,
        ITokenAppService tokenAppService) 
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _mediator = mediator;
        _tokenAppService = tokenAppService;
    }

    public async Task<string> LogIn(LoginViewModel loginViewModel) 
    {
        ValidateBeforeLogIn(loginViewModel);
        loginViewModel = OnBeforeLoginApply(loginViewModel);

        var user = await _userRepository.GetByEmail(loginViewModel.Email);

        ValidateAfterLogin(user, loginViewModel.Password);

        return _tokenAppService.GenerateToken(user);         
    }

    public async Task<Guid> Create(CreateNewAccountViewModel createNewAccountViewModel)
    {
        ValidateBeforeCreate(createNewAccountViewModel);
        createNewAccountViewModel = ApplyBeforeCreate(createNewAccountViewModel);

        var command = _mapper.Map<CreateUserCommand>(createNewAccountViewModel);

        return await _mediator.Send(command);
    }



    #region Validations and Applyers
    private static void ValidateBeforeLogIn(LoginViewModel loginViewModel)
    {
        if (loginViewModel == null) 
            throw 
                new ArgumentNullException(nameof(loginViewModel));

        if (string.IsNullOrWhiteSpace(loginViewModel.Email))
            throw new 
                InvalidOperationException($"{nameof(loginViewModel.Email)} cannot be empty");

        if (string.IsNullOrEmpty(loginViewModel.Password))
            throw new
                InvalidOperationException($"{nameof(loginViewModel.Password)} cannot be empty");
    }



    private static LoginViewModel OnBeforeLoginApply(LoginViewModel loginViewModel)
    {
        loginViewModel.Email = loginViewModel.Email.Trim();

        return loginViewModel;
    }

    private static void ValidateAfterLogin(User user, string unhashedPassword)
    {
        if (user is null || !BCryptHash.VerifyPassword(unhashedPassword, user.Password))
            throw new InvalidOperationException("Email or password is invalid");
    }


    private static void ValidateBeforeCreate(CreateNewAccountViewModel createNewAccount)
    {
        if (createNewAccount == null)
            throw new ArgumentNullException(nameof(createNewAccount));

        if (createNewAccount.ConfirmPassword != createNewAccount.Password)
            throw new InvalidOperationException("The passwords does not match");
    }

    private static CreateNewAccountViewModel ApplyBeforeCreate(CreateNewAccountViewModel createNewAccount)
    {
        createNewAccount.Email = createNewAccount.Email.Trim();
        createNewAccount.Name = createNewAccount.Name.Trim();

        return createNewAccount;
    }

    #endregion

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
