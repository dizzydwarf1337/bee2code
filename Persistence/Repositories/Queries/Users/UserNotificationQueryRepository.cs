using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.UsersCommands;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Queries.Users
{
    public class UserNotificationQueryRepository : RepositoryBase, IUserNotificationQueryRepository
    {
        public UserNotificationQueryRepository(BeeCodeDbContext context) : base(context)
        {
        }

        public async Task<UserNotification> GetUserNotificationByIdAsync(Guid userNotificationId)
        {
            return await _context.UserNotifications.FindAsync(userNotificationId) ?? throw new EntityNotFoundException("UserNotification");
        }

        public async Task<ICollection<UserNotification>> GetUserNotificationByUserIdAsync(Guid userId)
        {
            return await _context.UserNotifications.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
