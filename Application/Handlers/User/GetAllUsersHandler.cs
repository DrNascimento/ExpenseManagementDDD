using Application.DTO.Output.User;

namespace Application.Handlers.User;

public class GetAllUsersHandler(IUserRepository userRepository)
{
    public IEnumerable<UserOutput> Execute()
    {
        var users = userRepository.GetAll();
        return users.ToOutput();
    }
}
