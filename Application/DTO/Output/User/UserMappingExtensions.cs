namespace Application.DTO.Output.User;

public static class UserMappingExtensions
{
    public static UserOutput ToOutput(this Domain.Entities.User user) =>
        new(user.Id, user.Name, user.Email);

    public static IEnumerable<UserOutput> ToOutput(this IEnumerable<Domain.Entities.User> users) =>
        users.Select(ToOutput);
}
