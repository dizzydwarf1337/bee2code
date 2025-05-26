using Application.DTO.Users;
using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.EditUser;
using Application.Features.Users.Commands.GrantUserRole;
using Application.Features.Users.Commands.MarkNotificationRead;
using Application.Features.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users
{
    public class UserController : BaseController
    {
        [Authorize]
        [HttpDelete("delete/{deleteUserId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string deleteUserId)
        {
            return HandleResponse(await Mediator.Send(new DeleteUserCommand { UserId = deleteUserId }));
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserDto editUserDto)
        {
            return HandleResponse(await Mediator.Send(new EditUserCommand { editUserDto = editUserDto }));
        }
        [Authorize]
        [HttpPatch("readNotification/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead([FromRoute] string notificationId)
        {
            return HandleResponse(await Mediator.Send(new MarkNotificationReadCommand { notificationId = notificationId }));
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("role")]
        public async Task<IActionResult> GrantUserRole(GrandUserRoleDto grandUserRoleDto)
        {
            return HandleResponse(await Mediator.Send(new GrandUserRoleCommand { GrandUserRole = grandUserRoleDto }));
        }
        [Authorize(Roles ="Admin,Worker")]
        [HttpGet("{getUserId}")]
        public async Task<IActionResult> GetUserById([FromRoute] string getUserId)
        {
            return HandleResponse(await Mediator.Send(new GetUserByIdQuery { userId = getUserId }));
        }
    }
}
