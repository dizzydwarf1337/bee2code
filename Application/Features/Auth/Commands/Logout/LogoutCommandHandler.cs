using Application.Core.ApiResponse;
using Application.Features.Auth.Commands.Login;
using Application.Services.Interfaces.General;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<Unit>>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LogoutCommandHandler> _logger;

        public LogoutCommandHandler(IAuthService authService, ILogger<LogoutCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<ApiResponse<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _authService.LogOut(request.Email);
                return ApiResponse<Unit>.Success(Unit.Value);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure(ex.Message, 404);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure("Internal error", 500);
            }
        }
    }
}
