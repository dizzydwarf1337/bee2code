using Application.DTO.LabTesting;
using Application.DTO.Researches;
using Application.DTO.Users;
using Application.Services.Interfaces.General;
using Application.Services.Interfaces.Researches;
using AutoMapper;
using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.LinksCommands;
using Domain.Interfaces.Commands.ResearchesCommands;
using Domain.Interfaces.Queries.LabTestingQueries;
using Domain.Interfaces.Queries.LinksQueries;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Links;
using Domain.Models.Researches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.Researches
{
    public class ResearchService : IResearchService
    {
        private readonly IResearchCommandRepository _researchCommandRepository;
        private readonly IResearchQueryRepository _researchQueryRepository;
        private readonly IUserResearchCommandRepository _userResearchCommandRepository;
        private readonly IUserResearchQueryRepository _userResearchQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly ILabTestQueryRepository _labTestQueryRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ResearchService(IResearchCommandRepository researchCommandRepository,
            IResearchQueryRepository researchQueryRepository,
            IUserQueryRepository userQueryRepository,
            IMapper mapper,
            IFileService fileService,
            IUserResearchQueryRepository userResearchQueryRepository,
            IUserResearchCommandRepository userResearchCommandRepository,
            ILabTestQueryRepository labTestQueryRepository
            )
        {
            _researchCommandRepository = researchCommandRepository;
            _researchQueryRepository = researchQueryRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
            _fileService = fileService;
            _userResearchCommandRepository = userResearchCommandRepository;
            _userResearchQueryRepository = userResearchQueryRepository;
            _labTestQueryRepository = labTestQueryRepository;
        }


        // Commands
        public async Task AddUserToResearchAsync(CreateUserResearchDto userResearchDto)
        {
            var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(userResearchDto.ResearchId),null,"Admin");
            var user = await _userQueryRepository.GetUserByIdAsync(Guid.Parse(userResearchDto.UserId));
            if (research.Patients.Any(x => x.UserId == user.Id)) throw new EntityAlreadyExistsException("Patient in research");
            var userResearch = new UserResearch
            {
                UserId = user.Id,
                ResearchId = research.Id
            };
            var acceptancePath = await _fileService.SaveFile(userResearchDto.AcceptanceFile);
            userResearch.AccteptationFilePath = acceptancePath;
            await _userResearchCommandRepository.CreateUserResearchAsync(userResearch);

        }

        public async Task<ResearchDto> CreateResearchAsync(CreateResearchDto researchDto)
        {
            var owner = await _userQueryRepository.GetUserByIdAsync(Guid.Parse(researchDto.OwnerId));
            var research = _mapper.Map<Research>(researchDto);
            return _mapper.Map<ResearchDto>(await _researchCommandRepository.CreateResearchAsync(research));
        }

        public async Task RemoveUserResearch(RemoveUserResearchDto removeUserResearchDto)
        {
            var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(removeUserResearchDto.ResearchId),null,"Admin");
            var userResearch = await _userResearchQueryRepository.GetUserResearchByIdAsync(Guid.Parse(removeUserResearchDto.UserId), research.Id);
            await _userResearchCommandRepository.DeleteUserResearchAsync(research.Id, Guid.Parse(removeUserResearchDto.UserId));
            await _fileService.DeleteFile(userResearch.AccteptationFilePath);

        }

        public async Task<ResearchDto> UpdateResearchAsync(EditResearchDto editResearchDto)
        {
            var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(editResearchDto.Id), null, "Admin");
            _mapper.Map(editResearchDto, research);
            return _mapper.Map<ResearchDto>(await _researchCommandRepository.UpdateResearchAsync(research));
        }

        public async Task DeleteResearchAsync(Guid researchId)
        {
            await _researchCommandRepository.DeleteResearchAsync(researchId);
        }

        // Queries
        public async Task<ResearchDto> GetResearchByIdAsync(Guid reseachId, Guid? userId, string? userRole = "Patient")
        {
            var research = await _researchQueryRepository.GetResearchByIdAsync(reseachId, userId, userRole);
            var researchDto = new ResearchDto();
            _mapper.Map(research, researchDto);
            if(userRole != "Patient")
            {
                researchDto.LabTest = _mapper.Map<List<LabTestDto>>(await _labTestQueryRepository.GetLabTestsByResearchIdAsync(reseachId));
                researchDto.Patients = _mapper.Map<List<UserPreviewDto>>(await _userQueryRepository.GetUsersByResearchIdAsync(reseachId));
            }
            return researchDto;
        }

        public async Task<List<ResearchPreviewDto>> GetResearchesFiltredPaginated(Guid? ownerId = null, Guid? participantId = null, int? page = null, int? pageSize = null)
        {
            return _mapper.Map<List<ResearchPreviewDto>>(await _researchQueryRepository.GetResearchesFiltredPaginated(ownerId, participantId, page, pageSize));
        }

        public async Task<List<ResearchDto>> GetPatientResearchesPaginated(Guid patientId,int Page, int PageSize)
        {
            return _mapper.Map<List<ResearchDto>>(await _researchQueryRepository.GetResearchesByPatientIdPaginatedAsync(patientId, Page, PageSize));
        }
    }
            
}
