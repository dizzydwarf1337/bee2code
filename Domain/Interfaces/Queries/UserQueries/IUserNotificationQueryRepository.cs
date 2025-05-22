using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Queries.UserQueries
{
    public interface IUserNotificationQueryRepository
    {
        Task<ICollection<UserNotification>> GetUserNotificationByUserIdAsync(Guid userId);
        Task<UserNotification> GetUserNotificationByIdAsync(Guid userNotificationId);
    }
}
