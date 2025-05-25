using Application.Core.ApiResponse;
using Application.Features.Researches.Events.RemoveUserFromResearch;
using Application.Services.Interfaces.Researches;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Commands.RemoveUserFromResearch
{
    public class RemoveUserResearchCommandHandler : IRequestHandler<RemoveUserResearchCommand, ApiResponse<Unit>>
    {
        private readonly IResearchService _researchService;
        private readonly ILogger<RemoveUserResearchCommandHandler> _logger;
        private readonly IMediator _mediator;

        public RemoveUserResearchCommandHandler(IResearchService researchService, ILogger<RemoveUserResearchCommandHandler> logger, IMediator mediator)
        {
            _researchService = researchService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ApiResponse<Unit>> Handle(RemoveUserResearchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _researchService.RemoveUserResearch(request.RemoveUserResearchDto);
                await _mediator.Publish(new UserResearchRemovedEvent { userId = request.RemoveUserResearchDto.UserId, researchId = request.RemoveUserResearchDto.ResearchId }, cancellationToken);
                _logger.LogInformation($"User removed from research successfully. \n UserId: {request.RemoveUserResearchDto.UserId}\n ResearchId: {request.RemoveUserResearchDto.ResearchId}");
                return ApiResponse<Unit>.Success(Unit.Value, 200);
            }
            catch(EntityNotFoundException ex)
            {
                return ApiResponse<Unit>.Failure("UserResearch not found", 404);
            }
            catch(Exception ex)
            {
                return ApiResponse<Unit>.Failure("Internal error", 500);
            }
        }
    }
}
