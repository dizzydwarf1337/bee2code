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
        Task<ResearchDto> GetResearchByIdAsync(Guid reseachId, Guid? userId, string? userRole = "Patient");
        Task<List<ResearchPreviewDto>> GetResearchesFiltredPaginated(Guid? ownerId = null, Guid? participantId = null, int? page = null, int? pageSize = null);
        Task<List<ResearchDto>> GetPatientResearchesPaginated(Guid patientId, int Page, int PageSize);
        Task AddUserToResearchAsync(CreateUserResearchDto userResearch);
        Task RemoveUserResearch(RemoveUserResearchDto removeUserResearchDto);
    }
}
