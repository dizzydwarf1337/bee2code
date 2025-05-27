using Application.Services.Interfaces.Users;
using Domain.Interfaces.Queries.LabTestingQueries;
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

namespace Application.Features.LabTests.Events.LabTestDeleted
{
    public class LabTestDeletedEventHandler : INotificationHandler<LabTestDeletedEvent>
    {
        private readonly IUserNotificationService _userNotificationService;
        private readonly ILabTestQueryRepository _labTestQueryRepostiory;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ILogger<LabTestDeletedEventHandler> _logger;

        public LabTestDeletedEventHandler(IUserNotificationService userNotificationService, 
            ILabTestQueryRepository labTestQueryRepostiory, 
            IUserQueryRepository userQueryRepository,
            ILogger<LabTestDeletedEventHandler> logger)
        {
            _userNotificationService = userNotificationService;
            _labTestQueryRepostiory = labTestQueryRepostiory;
            _userQueryRepository = userQueryRepository;
            _logger = logger;
        }

        public async Task Handle(LabTestDeletedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var labTest = await _labTestQueryRepostiory.GetLabTestByIdAsync(Guid.Parse(notification.LabTestId),null,"Admin");
                var creator = await _userQueryRepository.GetUserByIdAsync(labTest.CreatorId);
                var userNotification = new UserNotification
                {
                    UserId = creator.Id,
                    Title = "Lab Test Deleted",
                    Message = $"The lab test '{labTest.Name}' has been deleted.",
                };
                await _userNotificationService.AddNotificationAsync(userNotification);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Error handling LabTestDeletedEvent for LabTestId: {LabTestId}", notification.LabTestId);
            }
        }
    }
}
