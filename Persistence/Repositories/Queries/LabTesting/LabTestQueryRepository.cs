using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Queries.LabTestingQueries;
using Domain.Models.LabTesting;
using Domain.Models.Researches;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Queries.LabTesting
{
    public class LabTestQueryRepository : RepositoryBase, ILabTestQueryRepository
    {
        public LabTestQueryRepository(BeeCodeDbContext context) : base(context)
        {
        }

        public async Task<LabTest> GetLabTestByIdAsync(Guid labTestId, Guid? userId, string? UserRole = "Patient")
        {
            IQueryable<LabTest> query = _context.LabTests;

            if (UserRole == "Admin" || UserRole=="Worker")
            {
                query = query.Where(x => x.Id == labTestId);

            }
            else 
            {
                query = query.Where(x => x.Id == labTestId && x.PatientId == userId);
            }
            var labTest = await query.Include(x=>x.LabTestResult).FirstOrDefaultAsync() ?? throw new EntityNotFoundException("LabTest");
            return labTest;

        }

        public async Task<ICollection<LabTest>> GetLabTestsByCreatorIdAsync(Guid creatorId)
        {
            var labTests = await _context.LabTests
                .Include(x => x.Patient)
                .Include(x => x.Creator)
                .Include(x => x.LabTestResult)
                .Where(x => x.CreatorId == creatorId)
                .ToListAsync();
            return labTests;
        }

        public async Task<ICollection<LabTest>> GetLabTestsByResearchIdAsync(Guid researchId)
        {
            var labTests = await _context.LabTests
             .Include(x => x.Patient)
             .Include(x => x.Creator)
             .Include(x => x.LabTestResult)
             .Where(x => x.ResearchId == researchId)
             .ToListAsync();
            return labTests;
        }

        public async Task<ICollection<LabTest>> GetLabTestsByUserIdAsync(Guid userId)
        {
            var labTests = await _context.LabTests
             .Include(x => x.Patient)
             .Include(x => x.Creator)
             .Include(x => x.LabTestResult)
             .Where(x => x.PatientId == userId)
             .ToListAsync();
            return labTests;
        }

        public async Task<ICollection<LabTest>> GetLabTestsPaginatedAsync(int page, int pageSize)
        {
            var labTests = await _context.LabTests
                 .Include(x => x.Patient)
                 .Include(x => x.Creator)
                 .Include(x => x.LabTestResult)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
            return labTests;
        }
    }
}
