using Application.Core.ApiResponse;
using Application.DTO.Users;
using Application.Services.Interfaces.General;
using Domain.Exceptions.BusinessExceptions;
using Domain.Exceptions.DataExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<UserDto>>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IAuthService authService, ILogger<LoginCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<ApiResponse<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userDto = await _authService.Login(request.loginDto);
                return ApiResponse<UserDto>.Success(userDto);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<UserDto>.Failure(ex.Message, 404);
            }
            catch (InvalidDataProvidedException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<UserDto>.Failure("Invalid password");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + ex.StackTrace);
                return ApiResponse<UserDto>.Failure("An error occurred while processing your request");
            }
        }
    }
}
