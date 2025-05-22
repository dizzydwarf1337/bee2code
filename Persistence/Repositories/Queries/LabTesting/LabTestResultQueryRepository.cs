using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Queries.LabTestingQueries;
using Domain.Models.LabTesting;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Queries.LabTesting
{
    public class LabTestResultQueryRepository : RepositoryBase, ILabTestResultQueryRepository
    {
        public LabTestResultQueryRepository(BeeCodeDbContext context) : base(context)
        {
        }

        public async Task<LabTestResult> GetLabTestResultByIdAsync(Guid labTestResultId)
        {
            return (await _context.LabTestResults
                .Include(x=>x.LabTest)
                .FirstOrDefaultAsync(x => x.Id == labTestResultId)) ?? throw new EntityNotFoundException("LabTestResult");
        }

        public async Task<ICollection<LabTestResult>> GetLabTestResultsByLabTestIdAsync(Guid labTestId)
        {
           var labTestResults = await _context.LabTestResults
                .Where(x => x.LabTestId == labTestId)
                .ToListAsync();
            return labTestResults;
        }

        public async Task<ICollection<LabTestResult>> GetLabTestResultsByUserIdAsync(Guid userId)
        {
            var labTestResults = await _context.LabTestResults
             .Include(x=>x.LabTest) 
             .Where(x => x.LabTest!.PatientId == userId)
             .ToListAsync();
            return labTestResults;
        }

        public async Task<ICollection<LabTestResult>> GetLabTestResultsPaginatedAsync(int page, int pageSize)
        {
            var labTestResults = await _context.LabTestResults
             .Include(x => x.LabTest)
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();
            return labTestResults;
        }
    }
}
