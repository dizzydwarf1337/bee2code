using Application.Core.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.MarkNotificationRead
{
    public class MarkNotificationReadCommand : IRequest<ApiResponse<Unit>>
    {
        public string notificationId { get; set; }
    }
}
