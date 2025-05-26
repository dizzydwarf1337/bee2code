using Application.Core.ApiResponse;
using Application.DTO.Researches;
using Application.Services.Interfaces.Researches;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Queries.GetResearchById
{
    public class GetResearchByIdQueryHandler : IRequestHandler<GetResearchByIdQuery, ApiResponse<ResearchDto>>
    {
        private readonly IResearchService _researchService;
        private readonly ILogger<GetResearchByIdQueryHandler> _logger;

        public GetResearchByIdQueryHandler(IResearchService researchService, ILogger<GetResearchByIdQueryHandler> logger)
        {
            _researchService = researchService;
            _logger = logger;
        }

        public async Task<ApiResponse<ResearchDto>> Handle(GetResearchByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return ApiResponse<ResearchDto>.Success(await _researchService.GetResearchByIdAsync(Guid.Parse(request.researchId), Guid.Parse(request.userId), request.userRole));
            }
            catch(EntityNotFoundException ex)
            {
                return ApiResponse<ResearchDto>.Failure("Research doesn't exists or you have no permisions to it",404);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Internal error: {ex.Message} {ex.StackTrace}");
                return ApiResponse<ResearchDto>.Failure("Internal error", 500);
            }
        }
    }
}
