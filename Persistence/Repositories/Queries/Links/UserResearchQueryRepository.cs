using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Queries.LinksQueries;
using Domain.Models.Links;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Queries.Links
{
    public class UserResearchQueryRepository : RepositoryBase, IUserResearchQueryRepository
    {
        public UserResearchQueryRepository(BeeCodeDbContext context) : base(context)
        {
        }

        public async Task<UserResearch> GetUserResearchByIdAsync(Guid userId, Guid researchId)
        {
            return (await _context.UserResearches.Include(x=>x.Research).FirstOrDefaultAsync(x=>x.UserId == userId && x.ResearchId == researchId)) ?? throw new EntityNotFoundException("UserResearch");   
        }

        public async Task<ICollection<UserResearch>> GetUserResearchesPaginatedAsync(int page, int pageSize)
        {
            return await _context.UserResearches.Include(x=>x.Research)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
