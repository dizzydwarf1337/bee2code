using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Models.Researches;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Queries.Researches
{
    public class ResearchQueryRepository : RepositoryBase, IResearchQueryRepository
    {
        public ResearchQueryRepository(BeeCodeDbContext context) : base(context)
        {
        }

        public async Task<Research> GetResearchByIdAsync(Guid reseachId)
        {
            return (await _context.Researches
                .Include(x => x.Patients)
                .Include(x => x.LabTests)
                .FirstOrDefaultAsync(x => x.Id == reseachId)) ?? throw new EntityNotFoundException("Research");
        }

        public async Task<ICollection<Research>> GetResearchesByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Researches
                .Include(x => x.Patients)
                .Include(x => x.LabTests)
                .Where(x => x.OwnerId == ownerId).ToListAsync();
        }

        public  async Task<ICollection<Research>> GetResearchesByUserIdAsync(Guid userId)
        {
            return await _context.Researches
               .Include(x => x.Patients)
               .Include(x => x.LabTests)
               .Where(x => x.Patients!.Any(x => x.UserId == userId)).ToListAsync();
        }

        public async Task<ICollection<Research>> GetResearchesPaginatedAsync(int page, int pageSize)
        {
            return await _context.Researches
                .Include(x => x.Patients)
                .Include(x => x.LabTests)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
    }
}
