using Application.DTO.Output.User;

namespace Application.Handlers.User;

public class GetUserByIdHandler(IUserRepository userRepository)
{
    public async Task<UserOutput> ExecuteAsync(Guid id)
    {
        var user = await userRepository.GetById(id);
        return user.ToOutput();
    }
}
