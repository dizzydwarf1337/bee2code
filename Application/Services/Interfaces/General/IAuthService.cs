using Application.DTO.General;
using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.General
{
    public interface IAuthService
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task Register(RegisterDto registerDto);
        Task LogOut(string userMail);
    }
}
