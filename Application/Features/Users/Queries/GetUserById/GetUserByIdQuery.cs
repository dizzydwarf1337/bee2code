using Application.Core.ApiResponse;
using Application.DTO.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<ApiResponse<UserDto>>
    {
        public string userId { get; set; }
    }
}
