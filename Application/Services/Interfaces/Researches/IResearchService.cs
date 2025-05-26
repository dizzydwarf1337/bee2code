using Application.DTO.Researches;
using Application.Features.Researches.Commands.RemoveUserFromResearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.Researches
{
    public  interface IResearchService
    {
        Task<ResearchDto> CreateResearchAsync(CreateResearchDto research);
        Task DeleteResearchAsync(Guid researchId);
        Task<ResearchDto> UpdateResearchAsync(EditResearchDto research);
        Task<ResearchDto> GetResearchByIdAsync(Guid reseachId);
        Task<ICollection<ResearchDto>> GetResearchesByOwnerIdAsync(Guid ownerId);
        Task<ICollection<ResearchDto>> GetResearchesByUserIdAsync(Guid userId);
        Task<ICollection<ResearchDto>> GetResearchesPaginatedAsync(int page, int pageSize);
        Task AddUserToResearchAsync(CreateUserResearchDto userResearch);
        Task RemoveUserResearch(RemoveUserResearchDto removeUserResearchDto);
    }
}
