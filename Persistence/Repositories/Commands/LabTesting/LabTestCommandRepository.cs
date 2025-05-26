using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.LabTestingCommands;
using Domain.Models.LabTesting;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commands.LabTesting
{
    public class LabTestCommandRepository : RepositoryBase, ILabTestCommandRepository
    {
       public LabTestCommandRepository(BeeCodeDbContext context) : base(context) 
        {

        }

        public async Task<LabTest> CreateLabTestAsync(LabTest labTest)
        {
            await _context.LabTests.AddAsync(labTest);
            await _context.SaveChangesAsync();
            return (await _context.LabTests
                .Include(x=>x.Patient)
                .Include(x=>x.Creator)
                .Include(x=>x.LabTestResult)
                .FirstAsync(l => l.Id == labTest.Id)) ?? throw new EntityCreatingException("LabTest","LabTestCommandRepository.CreateLabTestAsync");
        }

        public async Task DeleteLabTestAsync(Guid labTestId)
        {
            var labTest = await _context.LabTests.FirstOrDefaultAsync(l => l.Id == labTestId) ?? throw new EntityNotFoundException("LabTest");
            _context.LabTests.Remove(labTest);
            await _context.SaveChangesAsync();
        }

        public async Task<LabTest> UpdateLabTestAsync(LabTest labTest)
        {
            _context.Update(labTest);
            await _context.SaveChangesAsync();
            return labTest;
        }
    }
}
