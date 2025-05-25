using Application.Core.ApiResponse;
using Application.Features.Researches.Events.ResearchDeleted;
using Application.Services.Interfaces.Researches;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Commands.DeleteResearch
{
    public class DeleteResearchCommandHandler : IRequestHandler<DeleteResearchCommand, ApiResponse<Unit>>
    {
        private readonly IResearchService _researchService;
        private readonly IMediator _mediator;
        private readonly ILogger<DeleteResearchCommandHandler> _logger;

        public DeleteResearchCommandHandler(IResearchService researchService, IMediator mediator, ILogger<DeleteResearchCommandHandler> logger)
        {
            _researchService = researchService;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<ApiResponse<Unit>> Handle(DeleteResearchCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                _logger.LogInformation($"Research was deleted id: {request.researchId}");
                await _mediator.Publish(new ResearchDeletedEvent { researchId = request.researchId });
                await _researchService.DeleteResearchAsync(Guid.Parse(request.researchId));
                return ApiResponse<Unit>.Success(Unit.Value);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                return ApiResponse<Unit>.Failure("Internal error", 500);
            }
        }
    }
}
