using Domain.Interfaces.Commands.UsersCommands;
using Domain.Models.Users;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commands.Users
{
    public class UserNotificationCommandRepository : RepositoryBase, IUserNotificationCommandRepository
    {
        public UserNotificationCommandRepository(BeeCodeDbContext context) : base(context)
        {
        }

        public async Task AddNotificationAsync(UserNotification userNotification)
        {
            await _context.UserNotifications.AddAsync(userNotification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(Guid notificationId)
        {
            var notification = await _context.UserNotifications.FindAsync(notificationId) ?? throw new Exception("Notification not found");
            _context.UserNotifications.Remove(notification);
        }

        public Task MarkNotificationAsReadAsync(Guid notificationId)
        {
            var notification = _context.UserNotifications.Find(notificationId) ?? throw new Exception("Notification not found");
            notification.IsRead = true;
            _context.UserNotifications.Update(notification);
            return _context.SaveChangesAsync();
        }
    }
}
