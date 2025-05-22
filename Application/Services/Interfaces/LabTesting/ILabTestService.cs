using Application.DTO.LabTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.LabTesting
{
    public interface ILabTestService
    {
        Task<LabTestDto> CreateLabTestAsync(CreateLabTestDto labTest);
        Task DeleteLabTestAsync(Guid labTestId);
        Task<LabTestDto> UpdateLabTestAsync(EditLabTestDto editLabTestDto);
        Task<LabTestDto> GetLabTestByIdAsync(Guid labTestId);
        Task<ICollection<LabTestDto>> GetLabTestsByCreatorIdAsync(Guid creatorId);
        Task<ICollection<LabTestDto>> GetLabTestsByResearchIdAsync(Guid researchId);
        Task<ICollection<LabTestDto>> GetLabTestsByUserIdAsync(Guid userId);
        Task<ICollection<LabTestDto>> GetLabTestsPaginatedAsync(int page, int pageSize);
    }
}
