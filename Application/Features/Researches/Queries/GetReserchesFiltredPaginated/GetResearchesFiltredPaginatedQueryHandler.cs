using Application.Core.ApiResponse;
using Application.DTO.Researches;
using Application.Services.Interfaces.Researches;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Queries.GetReserchesFiltredPaginated
{
    public class GetResearchesFiltredPaginatedQueryHandler : IRequestHandler<GetResearchesFiltredPaginatedQuery, ApiResponse<List<ResearchPreviewDto>>>
    {
        private readonly IResearchService _researchService;
        private readonly ILogger<GetResearchesFiltredPaginatedQueryHandler> _logger;

        public GetResearchesFiltredPaginatedQueryHandler(IResearchService researchService, ILogger<GetResearchesFiltredPaginatedQueryHandler> logger)
        {
            _researchService = researchService;
            _logger = logger;
        }

        public async Task<ApiResponse<List<ResearchPreviewDto>>> Handle(GetResearchesFiltredPaginatedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Guid? ownerId = Guid.TryParse(request.ownerId, out var parsedOwnerId) ? parsedOwnerId : null;
                Guid? patientId = Guid.TryParse(request.patientId, out var parsedPatientId) ? parsedPatientId : null;
                return ApiResponse<List<ResearchPreviewDto>>.Success(await _researchService.GetResearchesFiltredPaginated(ownerId, patientId, request.Page, request.PageSize));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<List<ResearchPreviewDto>>.Failure("Internal error", 500);
            }
        }
    }
}
