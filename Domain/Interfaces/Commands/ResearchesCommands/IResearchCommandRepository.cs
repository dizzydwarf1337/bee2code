using Domain.Models.Researches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Commands.ResearchesCommands
{
    public interface IResearchCommandRepository
    {
        Task<Research> CreateResearchAsync(Research research);
        Task<Research> UpdateResearchAsync(Research research);
        Task DeleteResearchAsync(Guid researchId);

    }
}
