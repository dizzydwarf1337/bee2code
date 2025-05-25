using Application.DTO.Researches;
using Application.Services.Interfaces.General;
using Application.Services.Interfaces.Researches;
using AutoMapper;
using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.LinksCommands;
using Domain.Interfaces.Commands.ResearchesCommands;
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
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ResearchService(IResearchCommandRepository researchCommandRepository,
            IResearchQueryRepository researchQueryRepository,
            IUserQueryRepository userQueryRepository,
            IMapper mapper,
            IFileService fileService,
            IUserResearchQueryRepository userResearchQueryRepository,
            IUserResearchCommandRepository userResearchCommandRepository
            )
        {
            _researchCommandRepository = researchCommandRepository;
            _researchQueryRepository = researchQueryRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
            _fileService = fileService;
            _userResearchCommandRepository = userResearchCommandRepository;
            _userResearchQueryRepository = userResearchQueryRepository;
        }

        public async Task AddUserToResearchAsync(CreateUserResearchDto userResearchDto)
        {
            var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(userResearchDto.ResearchId));
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

        public Task DeleteResearchAsync(Guid researchId)
        {
            throw new NotImplementedException();
        }

        public Task<ResearchDto> GetResearchByIdAsync(Guid reseachId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ResearchDto>> GetResearchesByOwnerIdAsync(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ResearchDto>> GetResearchesByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ResearchDto>> GetResearchesPaginatedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveUserResearch(RemoveUserResearchDto removeUserResearchDto)
        {
            var research = await _researchQueryRepository.GetResearchByIdAsync(Guid.Parse(removeUserResearchDto.ResearchId));
            var userResearch = await _userResearchQueryRepository.GetUserResearchByIdAsync(Guid.Parse(removeUserResearchDto.UserId), research.Id);
            await _userResearchCommandRepository.DeleteUserResearchAsync(research.Id, Guid.Parse(removeUserResearchDto.UserId));
            await _fileService.DeleteFile(userResearch.AccteptationFilePath);

        }

        public Task<ResearchDto> UpdateResearchAsync(EditResearchDto research)
        {
            throw new NotImplementedException();
        }
    }
}
