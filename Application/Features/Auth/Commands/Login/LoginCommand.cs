using Application.Core.ApiResponse;
using Application.DTO.General;
using Application.DTO.Users;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<ApiResponse<UserDto>>
    {
        public LoginDto loginDto { get; set; }
    }
}
