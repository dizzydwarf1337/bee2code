using Application.Core.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest<ApiResponse<Unit>>
    {
        public string Email { get; set; }
    }
}
