using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Commands.UsersCommands
{
    public interface IUserNotificationCommandRepository
    {
        Task AddNotificationAsync(UserNotification userNotification);
        Task DeleteNotificationAsync(Guid notificationId);
        Task MarkNotificationAsReadAsync(Guid notificationId);
    }
}
