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

namespace Application.Features.Users.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly ILogger<EditUserCommandHandler> _logger;

        public EditUserCommandHandler(IUserService userService, ILogger<EditUserCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<ApiResponse<UserDto>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var user = await _userService.UpdateUser(request.editUserDto);
                _logger.LogInformation("User edited successfully userId: {Id}", user.Id);
                return ApiResponse<UserDto>.Success(user, 200);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing user");
                return ApiResponse<UserDto>.Failure("An error occurred while editing the user.", 500);
            }
        }
    }
}
