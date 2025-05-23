using Application.DTO.General;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Factories.Interfaces
{
    public interface IUserFactory
    {
        Task<User> CreateUserAsync(RegisterDto registerDto, string? UserRole = "User");
    }
}
