using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Commands.UsersCommands
{
    public interface IUserCommandRepository
    {
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
    }
}
