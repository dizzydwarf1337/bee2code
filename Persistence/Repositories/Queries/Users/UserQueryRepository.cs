using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Queries.Users
{
    public class UserQueryRepository : RepositoryBase, IUserQueryRepository
    {
        public UserQueryRepository(BeeCodeDbContext context) : base(context)
        {
        }

        public async Task<ICollection<User>> GetAllUsersPaginatedAsync(int page, int pageSize)
        {
            return await _context.Users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new EntityNotFoundException("User");
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return (await _context.Users
                .Include(x => x.LabTests)
                .Include(x => x.MyResearch)
                .Include(x => x.PatientResearches)
                .FirstOrDefaultAsync(x=>x.Id==userId))?? throw new EntityNotFoundException("User");
        }

        public async Task<ICollection<User>> GetUsersByResearchIdAsync(Guid researchId)
        {
            return await _context.Users.Include(x => x.PatientResearches).Where(x => x.PatientResearches.Any(x => x.ResearchId == researchId)).ToListAsync();
        }
    }
}
