using Application.Core.ApiResponse;
using Application.DTO.Users;
using Application.Services.Interfaces.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetAllUsersPaginated
{
    public class GetAllUsersPaginatedQueryHandler : IRequestHandler<GetAllUsersPaginatedQuery, ApiResponse<List<UserDto>>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetAllUsersPaginatedQueryHandler> _logger;

        public GetAllUsersPaginatedQueryHandler(IUserService userService, ILogger<GetAllUsersPaginatedQueryHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<ApiResponse<List<UserDto>>> Handle(GetAllUsersPaginatedQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                return ApiResponse<List<UserDto>>.Success(await _userService.GetAllUsersPaginatedAsync(request.page, request.pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<List<UserDto>>.Failure("Internal error", 500);
            }
        }
    }
}
