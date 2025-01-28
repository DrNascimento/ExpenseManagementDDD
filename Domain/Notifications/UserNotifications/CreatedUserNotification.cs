using Domain.Enums;
using MediatR;

namespace Domain.Notifications.UserNotifications;

public class CreatedUserNotification : INotification
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public UserTypeEnum UserTypeEnum { get; set; }

}
