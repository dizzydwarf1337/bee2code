using Application.Core.ApiResponse;
using Application.Services.Interfaces.General;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<Unit>>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(IAuthService authService, ILogger<RegisterCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<ApiResponse<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _authService.Register(request.RegisterDto);
                return ApiResponse<Unit>.Success(Unit.Value,201);
            }
            catch (EntityCreatingException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure("Internal error", 500);
            }
            catch (InvalidDataException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure("User with this email already exists", 400);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure("An error occurred while processing your request");

            }
        }
    }
}
