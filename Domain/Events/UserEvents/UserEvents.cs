using Domain.Commands.UserCommands;
using Domain.Notifications.UserNotifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events.UserEvents
{
    public class UserEvents : INotificationHandler<CreatedUserNotification>
    {
        public Task Handle(CreatedUserNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CRIACAO: '{notification.Id} - {notification.Name} - {notification.Email} - ***'");
            });
        }
    }
}
