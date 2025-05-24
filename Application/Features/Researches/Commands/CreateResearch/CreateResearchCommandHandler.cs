using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
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

namespace Application.Features.Researches.Commands.CreateResearch
{
    public class CreateResearchCommandHandler : IRequestHandler<CreateResearchCommand, ApiResponse<ResearchDto>>
    {
        private readonly IResearchService _researchService;
        private readonly ILogger<CreateResearchCommandHandler> _logger;

        public CreateResearchCommandHandler(IResearchService researchService, ILogger<CreateResearchCommandHandler> logger)
        {
            _researchService = researchService;
            _logger = logger;
        }

        public async Task<ApiResponse<ResearchDto>> Handle(CreateResearchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var research = await _researchService.CreateResearchAsync(request.createResearchDto);
                return ApiResponse<ResearchDto>.Success(research, 201);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<ResearchDto>.Failure(ex.Message, 404);
            }
            catch (EntityAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<ResearchDto>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<ResearchDto>.Failure("Something went wrong", 500);
            }
        }
    }
}
