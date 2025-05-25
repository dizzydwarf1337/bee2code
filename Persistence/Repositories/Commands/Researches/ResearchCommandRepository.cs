using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.ResearchesCommands;
using Domain.Models.Researches;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Commands.Researches
{
    public class ResearchCommandRepository : RepositoryBase, IResearchCommandRepository
    {
        public ResearchCommandRepository(BeeCodeDbContext context) : base(context) { }

        public async Task<Research> CreateResearchAsync(Research research)
        {
            await _context.Researches.AddAsync(research);
            await _context.SaveChangesAsync();
            return (await _context.Researches
                .Include(x=>x.Owner)
                .Include(x=>x.LabTests)
                .Include(x=>x.Patients)
                .FirstOrDefaultAsync(x=>x.Id==research.Id)) ?? throw new EntityCreatingException("Research","ResearchCommandRepository.CreateResearchAsync");
        }

        public async Task DeleteResearchAsync(Guid researchId)
        {
            var research = (await _context.Researches.FindAsync(researchId)) ?? throw new EntityNotFoundException("Research");
            _context.Researches.Remove(research);
            await _context.SaveChangesAsync();
        }

        public async Task<Research> UpdateResearchAsync(Research research)
        {
            _context.Researches.Update(research);
            await _context.SaveChangesAsync();
            return research; 
        }
    }
}
