using Domain.Exceptions.BusinessExceptions;
using Domain.Exceptions.DataExceptions;
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

        public async Task<Research> GetResearchByIdAsync(Guid reseachId, Guid? userId, string? userRole = "Patient")
        {
            var query = _context.Researches.AsQueryable();
            if(userRole == "Admin" || userRole == "Worker")
            {
                query = query.Include(x=>x.LabTests).Include(x=>x.Patients).Where(x => x.Id == reseachId);
            }
            else
            {
                query = query.Include(x => x.LabTests.Where(x => x.PatientId == userId)).Where(x => x.Id == reseachId);
            }
            var research = await query.FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Research");
            return research;
        }

        public async Task<ICollection<Research>> GetResearchesByOwnerIdPaginatedAsync(Guid ownerId, int Page, int PageSize)
        {
            return await _context.Researches
                .Include(x => x.Patients)
                .Include(x => x.LabTests)
                .Where(x => x.OwnerId == ownerId).ToListAsync();
        }

        public  async Task<ICollection<Research>> GetResearchesByPatientIdPaginatedAsync(Guid userId, int Page, int PageSize)
        {
            return await _context.Researches
               .Include(x => x.LabTests.Where(x=>x.PatientId==userId).OrderBy(x=>x.CreatedAt))
               .Where(x => x.Patients!.Any(x => x.UserId == userId))
               .Skip((Page-1)*PageSize)
               .Take(PageSize)
               .OrderByDescending(x=>x.CreatedAt)
               .ToListAsync();
        }

        public async Task<ICollection<Research>> GetResearchesFiltredPaginated(Guid? ownerId, Guid? patientId, int? Page, int? PageSize)
        {
            var query = _context.Researches.AsQueryable();

            if (ownerId != null)
            {
                query = query.Where(x => x.OwnerId == ownerId);
            }
            if (patientId != null)
            {
                query = query.Include(x => x.Patients)
                             .Where(x => x.Patients.Any(p => p.UserId == patientId));
            }

            if (Page != null && PageSize != null)
            {
                if (PageSize > 0 && Page >= 0)
                {
                    query = query.Skip(((int)Page - 1) * (int)PageSize)
                                 .Take((int)PageSize);
                }
                else throw new InvalidDataProvidedException("Wrong page or page size provided");
            }
            return await query.ToListAsync();
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
