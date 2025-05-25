using Application.Services.Interfaces.Users;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Events.ResearchDeleted
{
    public class ResearchDeletedEventHandler : INotificationHandler<ResearchDeletedEvent>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IResearchQueryRepository _researchQueryRepository;
        private readonly IUserNotificationService _notificationService;
        private readonly ILogger<ResearchDeletedEventHandler> _logger;
        public ResearchDeletedEventHandler(IUserQueryRepository userQueryRepository, 
            IResearchQueryRepository researchQueryRepository, 
            IUserNotificationService notificationService,
            ILogger<ResearchDeletedEventHandler> logger)
        {
            _userQueryRepository = userQueryRepository;
            _researchQueryRepository = researchQueryRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Handle(ResearchDeletedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(notification.researchId));
                var worker = await _userQueryRepository.GetUserByIdAsync(research.OwnerId);
                var patients = await _userQueryRepository.GetUsersByResearchIdAsync(research.Id);
                var workerNotification = new UserNotification
                {
                    UserId = worker.Id,
                    Title = "Research was deleted",
                    Message = $"Your research {research.Name} was deleted successfully"
                };
                await _notificationService.AddNotificationAsync(workerNotification);
                foreach (var patient in patients)
                {
                    var userNotification = new UserNotification
                    {
                        UserId = patient.Id,
                        Title = "Research has ended",
                        Message = $"You participatin in research {research.Name} was ended",
                    };
                    await _notificationService.AddNotificationAsync (userNotification);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message + ex.StackTrace);
            }
        }
    }
}
