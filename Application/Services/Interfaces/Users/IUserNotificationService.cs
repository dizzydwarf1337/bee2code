using Application.DTO.Users;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.Users
{
    public interface IUserNotificationService
    {
        Task AddNotificationAsync(UserNotification userNotification);
        Task DeleteNotificationAsync(Guid notificationId);
        Task MarkNotificationAsReadAsync(Guid notificationId);
        Task<ICollection<UserNotificationDto>> GetUserNotificationByIdAsync(Guid userNotificationId);
        Task<ICollection<UserNotificationDto>> GetUserNotificationByUserIdAsync(Guid userId);
    }
}
