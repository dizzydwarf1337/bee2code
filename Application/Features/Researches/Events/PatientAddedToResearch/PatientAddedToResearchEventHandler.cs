using Application.DTO.Users;
using Application.Services.Interfaces.Researches;
using Application.Services.Interfaces.Users;
using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.UsersCommands;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Events.PatientAddedToResearch
{
    public class PatientAddedToResearchEventHandler : INotificationHandler<PatientAddedToResearchEvent>
    {
        private readonly IUserNotificationCommandRepository _userNotificationCommandRepository;
        private readonly IUserQueryRepository _userQueryRepostiory;
        private readonly IResearchQueryRepository _researchQueryRepository;

        public PatientAddedToResearchEventHandler(IUserNotificationCommandRepository userNotificationCommandRepository, IUserQueryRepository userQueryRepostiory, IResearchQueryRepository researchQueryRepository)
        {
            _userNotificationCommandRepository = userNotificationCommandRepository;
            _userQueryRepostiory = userQueryRepostiory;
            _researchQueryRepository = researchQueryRepository;
        }

        public async Task Handle(PatientAddedToResearchEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userQueryRepostiory.GetUserByIdAsync(Guid.Parse(notification.userId));
                var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(notification.researchId));
                var usNotification = new UserNotification
                {
                    UserId = user.Id,
                    Message = $"You have been added to research: {research.Name}",
                    Title = "New research",
                };
                await _userNotificationCommandRepository.AddNotificationAsync(usNotification);
                var workerNotification = new UserNotification
                {
                    UserId = research.OwnerId,
                    Message = $"{user.FirstName} {user.LastName} has been added to the research: {research.Name}",
                    Title = "New patient in research",
                };
                await _userNotificationCommandRepository.AddNotificationAsync(workerNotification);
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            catch (EntityAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }
    }
}
