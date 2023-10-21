using Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notifications.UserNotifications
{
    public class CreatedUserNotification : INotification
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserTypeEnum UserTypeEnum { get; set; }

    }
}
