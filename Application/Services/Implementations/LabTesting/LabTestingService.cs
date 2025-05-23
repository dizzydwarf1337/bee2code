using Application.DTO.LabTesting;
using Application.Services.Interfaces.LabTesting;
using Domain.Interfaces.Commands.LabTestingCommands;
using Domain.Interfaces.Queries.LabTestingQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.LabTesting
{
    public class LabTestingService : ILabTestService
    {
        private readonly ILabTestCommandRepository _labTestCommandRepository;
        private readonly ILabTestQueryRepository _labTestQueryRepository;

        public LabTestingService(ILabTestCommandRepository labTestCommandRepository, ILabTestQueryRepository labTestQueryRepository)
        {
            _labTestCommandRepository = labTestCommandRepository;
            _labTestQueryRepository = labTestQueryRepository;
        }

        public Task<LabTestDto> CreateLabTestAsync(CreateLabTestDto labTest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLabTestAsync(Guid labTestId)
        {
            throw new NotImplementedException();
        }

        public Task<LabTestDto> GetLabTestByIdAsync(Guid labTestId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<LabTestDto>> GetLabTestsByCreatorIdAsync(Guid creatorId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<LabTestDto>> GetLabTestsByResearchIdAsync(Guid researchId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<LabTestDto>> GetLabTestsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<LabTestDto>> GetLabTestsPaginatedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<LabTestDto> UpdateLabTestAsync(EditLabTestDto editLabTestDto)
        {
            throw new NotImplementedException();
        }
    }
}
