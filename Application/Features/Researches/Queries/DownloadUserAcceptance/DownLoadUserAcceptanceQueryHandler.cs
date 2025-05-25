using Application.DTO.Researches;
using Application.Services.Implementations.Researches;
using Application.Services.Interfaces.General;
using Application.Services.Interfaces.Researches;
using Application.Services.Interfaces.Users;
using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Queries.LinksQueries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Queries.DownloadUserAcceptance
{
    public class DownLoadUserAcceptanceQueryHandler : IRequestHandler<DownloadUserAcceptanceQuery, AcceptanceDownloadDto>
    {
        private readonly ILogger<DownLoadUserAcceptanceQueryHandler> _logger;
        private readonly IFileService _fileService;
        private readonly IUserResearchQueryRepository _userResearchQueryRepository;
        public DownLoadUserAcceptanceQueryHandler(IFileService fileService, IUserResearchQueryRepository userResearchQueryRepository,ILogger<DownLoadUserAcceptanceQueryHandler> logger)
        {
            _fileService = fileService;
            _userResearchQueryRepository = userResearchQueryRepository;
            _logger = logger;
        }

        public async Task<AcceptanceDownloadDto> Handle(DownloadUserAcceptanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Downloading information for  userId: {request.userId} \n researchId: {request.researchId}");
                var userResearch = await _userResearchQueryRepository.GetUserResearchByIdAsync(Guid.Parse(request.userId), Guid.Parse(request.researchId));
                if (String.IsNullOrEmpty(userResearch.AccteptationFilePath))
                    return new AcceptanceDownloadDto();
                else
                {
                    return await _fileService.DownloadFile(userResearch.AccteptationFilePath);
                }
            }
            catch(EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return new AcceptanceDownloadDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return new AcceptanceDownloadDto();
            }
        }
    }
}
