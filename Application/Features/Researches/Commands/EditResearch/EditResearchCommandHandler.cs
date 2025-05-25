using Application.Core.ApiResponse;
using Application.DTO.Researches;
using Application.Services.Interfaces.Researches;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Commands.EditResearch
{
    public class EditResearchCommandHandler : IRequestHandler<EditResearchCommand, ApiResponse<ResearchDto>>
    {
        private readonly IResearchService _researchService;
        private readonly ILogger<EditResearchCommandHandler> _logger;

        public EditResearchCommandHandler(IResearchService researchService, ILogger<EditResearchCommandHandler> logger)
        {
            _researchService = researchService;
            _logger = logger;
        }

        public async Task<ApiResponse<ResearchDto>> Handle(EditResearchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var editedResearch = await _researchService.UpdateResearchAsync(request.EditResearchDto);
                _logger.LogInformation($"Research was updated {editedResearch.Id}");
                return ApiResponse<ResearchDto>.Success( editedResearch );
            }
            catch(EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<ResearchDto>.Failure(ex.Message,404);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<ResearchDto>.Failure("Internal error", 500);
            }
        }
    }
}
