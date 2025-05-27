using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.UsersCommands;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commands.Users
{
    public class UserCommandRepository : RepositoryBase, IUserCommandRepository
    {
        public UserCommandRepository(BeeCodeDbContext context) : base(context)
        {
        }


        public async Task DeleteUserAsync(Guid userId)
        {
            var user = (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)) ?? throw new EntityNotFoundException("User");
            await _context.LabTests
                .Where(x => x.PatientId == userId)
                .ExecuteDeleteAsync(); 

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;

        }
    }
}
