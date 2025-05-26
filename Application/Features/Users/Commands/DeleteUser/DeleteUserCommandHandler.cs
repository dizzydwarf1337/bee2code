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

namespace Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<Unit>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserService userService, ILogger<DeleteUserCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<ApiResponse<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteUser(Guid.Parse(request.UserId));
                _logger.LogInformation("User with ID {UserId} deleted successfully", request.UserId);
                return ApiResponse<Unit>.Success(Unit.Value);
            }
            catch(EntityNotFoundException ex)
            {
                _logger.LogError(ex, "User with ID {UserId} not found", request.UserId);
                return ApiResponse<Unit>.Failure($"User with ID {request.UserId} not found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user with ID {UserId}", request.UserId);
                return ApiResponse<Unit>.Failure("An error occurred while deleting the user.", 500);
            }
        }
    }
}
