using Application.Core.ApiResponse;
using Application.DTO.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.GrantUserRole
{
    public class GrandUserRoleCommand :IRequest<ApiResponse<Unit>>
    {
       public GrandUserRoleDto GrandUserRole { get; set; }
    }
}
