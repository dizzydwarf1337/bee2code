using Application.Core.ApiResponse;
using Application.Services.Interfaces.Users;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.GrantUserRole
{
    public class GrandUserRoleCommandHandler : IRequestHandler<GrandUserRoleCommand, ApiResponse<Unit>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<GrandUserRoleCommandHandler> _logger;

        public GrandUserRoleCommandHandler(IUserService userService, ILogger<GrandUserRoleCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<ApiResponse<Unit>> Handle(GrandUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.GrantUserRole(Guid.Parse(request.GrandUserRole.userId), request.GrandUserRole.role);
                return ApiResponse<Unit>.Success(Unit.Value);
            }
            catch(EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure(ex.Message,404);
            }
            catch (Exception ex) 
            {

                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure("Internal error",500);
            }
        }
    }
}
