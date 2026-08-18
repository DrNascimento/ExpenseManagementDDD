using Application.DTO.Output.User;

namespace Application.Handlers.User;

public class GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
{
    public IEnumerable<UserOutput> Execute()
    {
        var users = userRepository.GetAll();
        return mapper.Map<IEnumerable<UserOutput>>(users);
    }
}
