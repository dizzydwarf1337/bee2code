using Application.DTO.Users;
using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.EditUser;
using Application.Features.Users.Commands.MarkNotificationRead;
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

        
    }
}
