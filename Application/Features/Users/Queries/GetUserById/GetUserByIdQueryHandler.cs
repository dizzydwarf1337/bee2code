using Application.Core.ApiResponse;
using Application.DTO.Users;
using Application.Services.Interfaces.Users;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(IUserService userService, ILogger<GetUserByIdQueryHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return ApiResponse<UserDto>.Success(await _userService.GetUserByIdAsync(Guid.Parse(request.userId)));
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<UserDto>.Failure(ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<UserDto>.Failure("Internal error", 500);
            }
        }
    }
}
