using Application.Core.ApiResponse;
using Application.Services.Interfaces.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.MarkNotificationRead
{
    public class MarkNotificationReadCommandHandler : IRequestHandler<MarkNotificationReadCommand, ApiResponse<Unit>>
    {
        private readonly IUserNotificationService _userNotificationService;

        public MarkNotificationReadCommandHandler(IUserNotificationService userNotificationService)
        {
            _userNotificationService = userNotificationService;
        }

        public async Task<ApiResponse<Unit>> Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userNotificationService.MarkNotificationAsReadAsync(Guid.Parse(request.notificationId));
                return ApiResponse<Unit>.Success(Unit.Value);
            }
            catch(Exception ex)
            {
                return ApiResponse<Unit>.Failure("An error occurred while marking the notification as read.", 500);
            }
        }
    }
}
