using Domain.Interfaces.Commands.UsersCommands;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Events.RemoveUserFromResearch
{
    public class UserResearchRemovedEventHandler : INotificationHandler<UserResearchRemovedEvent>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IResearchQueryRepository _researchQueryRepository;
        private readonly IUserNotificationCommandRepository _userNotificationCommandRepository;
        private readonly ILogger<UserResearchRemovedEventHandler> _logger;

        public UserResearchRemovedEventHandler(IUserQueryRepository userQueryRepository,
            IResearchQueryRepository researchQueryRepository,
            IUserNotificationCommandRepository userNotificationCommandRepository,
            ILogger<UserResearchRemovedEventHandler> logger)
        {
            _userQueryRepository = userQueryRepository;
            _researchQueryRepository = researchQueryRepository;
            _userNotificationCommandRepository = userNotificationCommandRepository;
            _logger = logger;
        }

        public async Task Handle(UserResearchRemovedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userQueryRepository.GetUserByIdAsync(Guid.Parse(notification.userId));
                var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(notification.researchId));
                var userNotification = new UserNotification
                {
                    UserId = user.Id,
                    Title = "You has been removed from research",
                    Message = $"Dear {user.FirstName}, you have been removed from the research: {research.Name}",
                };
                await _userNotificationCommandRepository.AddNotificationAsync(userNotification);
                var workerNotification = new UserNotification
                {
                    UserId = research.OwnerId,
                    Title = "User removed from research",
                    Message = $"{user.FirstName} {user.LastName} has been removed from the research: {research.Name}",
                };
                await _userNotificationCommandRepository.AddNotificationAsync(workerNotification);
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace, "Error handling UserResearchRemovedEvent");
            }
        }
    }
}
