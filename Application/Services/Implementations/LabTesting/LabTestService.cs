using Application.DTO.LabTesting;
using Application.Services.Interfaces.LabTesting;
using AutoMapper;
using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.LabTestingCommands;
using Domain.Interfaces.Queries.LabTestingQueries;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.LabTesting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.LabTesting
{
    public class LabTestService : ILabTestService
    {
        private readonly ILabTestCommandRepository _labTestCommandRepository;
        private readonly ILabTestQueryRepository _labTestQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IResearchQueryRepository _researchQueryRepository;
        private readonly IMapper _mapper;


        public LabTestService(
            ILabTestCommandRepository labTestCommandRepository, 
            ILabTestQueryRepository labTestQueryRepository, 
            IUserQueryRepository userQueryRepository,
            IResearchQueryRepository researchQueryRepository,
            IMapper mapper
            )
        {
            _labTestCommandRepository = labTestCommandRepository;
            _labTestQueryRepository = labTestQueryRepository;
            _userQueryRepository = userQueryRepository;
            _researchQueryRepository = researchQueryRepository;
            _mapper = mapper;
        }

        // Commands
        public async Task<LabTestDto> CreateLabTestAsync(CreateLabTestDto labTestDto)
        {
            var patient = await _userQueryRepository.GetUserByIdAsync(Guid.Parse(labTestDto.PatientId));
            var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(labTestDto.ResearchId));

            if (!research.Patients.Any(x=>x.UserId==patient.Id)) throw new EntityNotFoundException("User in research");
            
            var worker = await _userQueryRepository.GetUserByIdAsync(Guid.Parse(labTestDto.CreatorId));
            var labTest = _mapper.Map<LabTest>(labTestDto);
            labTest.Creator = worker;
            labTest.Patient = patient;
            labTest.Research = research;

            return _mapper.Map<LabTestDto>(await _labTestCommandRepository.CreateLabTestAsync(labTest));

        }
        public async Task<LabTestDto> UpdateLabTestAsync(EditLabTestDto editLabTestDto)
        {
            var labTest = await _labTestQueryRepository.GetLabTestByIdAsync(Guid.Parse(editLabTestDto.Id));
            _mapper.Map(editLabTestDto, labTest);
            return _mapper.Map<LabTestDto>(await _labTestCommandRepository.UpdateLabTestAsync(labTest));
        }

        public async Task DeleteLabTestAsync(Guid labTestId)
        {
            await _labTestCommandRepository.DeleteLabTestAsync(labTestId);
        }



        // Queries
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


    }
}
