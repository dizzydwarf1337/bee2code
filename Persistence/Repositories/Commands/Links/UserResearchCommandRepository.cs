using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.LinksCommands;
using Domain.Models.Links;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commands.Links
{
    public class UserResearchCommandRepository : RepositoryBase, IUserResearchCommandRepository
    {
        public UserResearchCommandRepository(BeeCodeDbContext context) : base(context)
        {
        }

        public async Task<UserResearch> CreateUserResearchAsync(UserResearch userResearch)
        {
            await _context.UserResearches.AddAsync(userResearch);
            await _context.SaveChangesAsync();
            return await _context.UserResearches
                .Include(x=>x.User)
                .Include(x=>x.Research)
                .FirstOrDefaultAsync(x=>x.UserId == userResearch.UserId && x.ResearchId==userResearch.ResearchId) ?? throw new EntityCreatingException("UserResearch","UserResearchCommandRepository.CreateUserResearchAsync"); 
        }

        public async Task DeleteUserResearchAsync(Guid researchId, Guid userId)
        {
            var userResearch = await _context.UserResearches.FirstOrDefaultAsync(x=>x.UserId==userId && x.ResearchId==researchId) ?? throw new EntityNotFoundException("UserResearch");
            _context.UserResearches.Remove(userResearch);
            await _context.SaveChangesAsync();
        }
    }
}
