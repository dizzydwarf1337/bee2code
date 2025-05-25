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
    public class LabTestResultCommandRepository : RepositoryBase, ILabTestResultCommandRepository
    {
        public LabTestResultCommandRepository(BeeCodeDbContext context) : base(context) { }

        public async Task<LabTestResult> CreateLabTestResultAsync(LabTestResult labTestResult)
        {
            await _context.LabTestResults.AddAsync(labTestResult);
            await _context.SaveChangesAsync();
            return (await _context.LabTestResults
                .Include(x=>x.LabTest)
                .FirstOrDefaultAsync(x => x.Id == labTestResult.Id))?? throw new EntityCreatingException("LabTestResult","LabTestResultCommandRepository.CreateLabTestResultAsync");
        }

        public async Task DeleteLabTestResultAsync(Guid labTestResultId)
        {
           var labTestResult = await _context.LabTestResults.FirstOrDefaultAsync(x => x.Id == labTestResultId) ?? throw new EntityNotFoundException("LabTestResult");
            _context.LabTestResults.Remove(labTestResult);
            await _context.SaveChangesAsync();
        }

        public async Task<LabTestResult> UpdateLabTestResultAsync(LabTestResult labTestResult)
        {
            _context.LabTestResults.Update(labTestResult);
            await _context.SaveChangesAsync();
            return labTestResult;
        }
    }
}
