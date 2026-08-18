using Application.DTO.Output.User;

namespace Application.Handlers.User;

public class GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
{
    public async Task<UserOutput> ExecuteAsync(Guid id)
    {
        var user = await userRepository.GetById(id);
        return mapper.Map<UserOutput>(user);
    }
}
