using Application.Core.ApiResponse;
using Application.DTO.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedQuery : IRequest<ApiResponse<List<UserDto>>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
