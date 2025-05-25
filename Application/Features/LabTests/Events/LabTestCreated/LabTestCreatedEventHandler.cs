using Application.Features.Researches.Events.PatientAddedToResearch;
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

namespace Application.Features.LabTests.Events.LabTestCreated
{
    public class LabTestCreatedEventHandler : INotificationHandler<LabTestCreatedEvent>
    {
        private readonly IUserNotificationCommandRepository _userNotificationCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ILogger<LabTestCreatedEventHandler> _logger;
        private readonly IResearchQueryRepository _researchQueryRepository;
        public LabTestCreatedEventHandler(IUserNotificationCommandRepository userNotificationCommandRepository, 
            IUserQueryRepository userQueryRepository,
            ILogger<LabTestCreatedEventHandler> logger,
            IResearchQueryRepository researchQueryRepository)
        {
            _userNotificationCommandRepository = userNotificationCommandRepository;
            _userQueryRepository = userQueryRepository;
            _logger = logger;
            _researchQueryRepository = researchQueryRepository;
        }

        public async Task Handle(LabTestCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _userQueryRepository.GetUserByIdAsync(Guid.Parse(notification.patientId));
                var reserach = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(notification.researchId));
                var usserNotification = new UserNotification
                {
                    UserId = patient.Id,
                    Message = $"Dear {patient.FirstName}, a new lab test has been created for you in the research: {reserach.Name}.",
                    Title = "New Lab Test",
                    
                };
                await _userNotificationCommandRepository.AddNotificationAsync(usserNotification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling LabTestCreatedEvent for patient {PatientId} in research {ResearchId}", notification.patientId, notification.researchId);
            }
        }
    }
}
