using Application.Core.ApiResponse;
using Application.DTO.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<ApiResponse<UserDto>>
    {
        public EditUserDto editUserDto { get; set; }
    }
}
