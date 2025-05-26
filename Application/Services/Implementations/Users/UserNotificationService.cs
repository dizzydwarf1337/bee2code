using Application.DTO.Users;
using Application.Services.Interfaces.Users;
using Domain.Interfaces.Commands.UsersCommands;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.Users
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUserNotificationCommandRepository _userNotificationCommandRepository;
        private readonly IUserNotificationQueryRepository _userNotificationQueryRepository;

        public UserNotificationService(IUserNotificationCommandRepository userNotificationCommandRepository, IUserNotificationQueryRepository userNotificationQueryRepository)
        {
            _userNotificationCommandRepository = userNotificationCommandRepository;
            _userNotificationQueryRepository = userNotificationQueryRepository;
        }

        public async Task AddNotificationAsync(UserNotification userNotification)
        {
            await _userNotificationCommandRepository.AddNotificationAsync(userNotification);
        }

        public async Task DeleteNotificationAsync(Guid notificationId)
        {
            await _userNotificationCommandRepository.DeleteNotificationAsync(notificationId);
        }

        public Task<ICollection<UserNotificationDto>> GetUserNotificationByIdAsync(Guid userNotificationId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserNotificationDto>> GetUserNotificationByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task MarkNotificationAsReadAsync(Guid notificationId)
        {
            await _userNotificationCommandRepository.MarkNotificationAsReadAsync(notificationId);
        }
    }
}
