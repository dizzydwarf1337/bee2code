using Application.DTO.General;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
using Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Auth
{
    public class AuthController : BaseController
    {


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return HandleResponse(await Mediator.Send(new LoginCommand { loginDto = loginDto})); 
        }
        [HttpPost("register")]  
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            return HandleResponse(await Mediator.Send(new RegisterCommand { RegisterDto = registerDto }));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] EmailDto emailDto)
        {
            return HandleResponse(await Mediator.Send(new LogoutCommand { Email = emailDto.Email}));
        }
    }
}
