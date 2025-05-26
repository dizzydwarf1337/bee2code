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

namespace Application.Features.Researches.Queries.GetPatientResearchesPaginated
{
    public class GetPatientResearchesPaginatedQueryHanlder : IRequestHandler<GetPatientResearchesPaginatedQuery, ApiResponse<List<ResearchDto>>>
    {
        private readonly IResearchService _researchService;
        private readonly ILogger<GetPatientResearchesPaginatedQueryHanlder> _logger;

        public GetPatientResearchesPaginatedQueryHanlder(IResearchService researchService, ILogger<GetPatientResearchesPaginatedQueryHanlder> logger)
        {
            _researchService = researchService;
            _logger = logger;
        }

        public async Task<ApiResponse<List<ResearchDto>>> Handle(GetPatientResearchesPaginatedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return ApiResponse<List<ResearchDto>>.Success(await _researchService.GetPatientResearchesPaginated(Guid.Parse(request.patientId), request.page, request.pageSize));
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<List<ResearchDto>>.Failure("Internal error", 400);

            }
        }
    }
}
