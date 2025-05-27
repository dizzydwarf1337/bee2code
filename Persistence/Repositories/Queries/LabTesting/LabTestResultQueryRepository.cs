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

    }
}
