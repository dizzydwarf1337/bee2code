using Domain.Interfaces.Commands.UsersCommands;
using Domain.Interfaces.Queries.LabTestingQueries;
using Domain.Models.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Events.LabTestResultCreated
{
    public class LabTestResultCreatedEventHandler : INotificationHandler<LabTestResultCreatedEvent>
    {
        private readonly IUserNotificationCommandRepository _userNotificationCommandRepository;
        private readonly ILabTestQueryRepository _labTestQueryRepository;
        private readonly ILogger<LabTestResultCreatedEventHandler> _logger;
        public LabTestResultCreatedEventHandler(IUserNotificationCommandRepository userNotificationCommandRepository,
            ILabTestQueryRepository labTestQueryRepository,
            ILogger<LabTestResultCreatedEventHandler> logger)
        {
            _userNotificationCommandRepository = userNotificationCommandRepository;
            _labTestQueryRepository = labTestQueryRepository;
            _logger = logger;
        }

        public async Task Handle(LabTestResultCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var labTest = await _labTestQueryRepository.GetLabTestByIdAsync(Guid.Parse(notification.labTestId));
                var userNotification = new UserNotification
                {
                    UserId = labTest.PatientId,
                    Title = "Test result is ready",
                    Message = $"Your test result for {labTest.Name} is ready"
                };
                await _userNotificationCommandRepository.AddNotificationAsync(userNotification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
            }
        }
    }
}
