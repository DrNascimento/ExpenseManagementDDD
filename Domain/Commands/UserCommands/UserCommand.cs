
using Domain.Enums;

namespace Domain.Commands.UserCommands;

public class UserCommand 
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public UserTypeEnum UserTypeEnum { get; set; }
    
}
