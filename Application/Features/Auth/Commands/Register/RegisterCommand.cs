using Application.Core.ApiResponse;
using Application.DTO.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<ApiResponse<Unit>>
    {
        public RegisterDto RegisterDto { get; set; }
    }
}
