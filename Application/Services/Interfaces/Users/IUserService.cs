using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.Users
{
    public interface IUserService 
    {
        Task DeleteUser(Guid userId);
        Task<UserDto> UpdateUser(EditUserDto userDto);
        Task<UserDto> GetUserByIdAsync(Guid userId);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<ICollection<UserDto>> GetUsersByResearchIdAsync(Guid researchId);
        Task<ICollection<UserDto>> GetAllUsersPaginatedAsync(int page, int pageSize);
    }
}
