using Application.Core.ApiResponse;
using Application.Features.Researches.Events.PatientAddedToResearch;
using Application.Services.Interfaces.Researches;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Commands.AddUserToResearch
{
    public class AddUserToResearchCommandHandler : IRequestHandler<AddUserToResearchCommand, ApiResponse<Unit>>
    {
        private readonly IResearchService _researchService;
        private readonly ILogger<AddUserToResearchCommandHandler> _logger;
        private readonly IMediator _mediator;

        public AddUserToResearchCommandHandler(IResearchService researchService, ILogger<AddUserToResearchCommandHandler> logger, IMediator mediator)
        {
            _researchService = researchService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ApiResponse<Unit>> Handle(AddUserToResearchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _researchService.AddUserToResearchAsync(request.userResearchDto);
                await _mediator.Publish(new PatientAddedToResearchEvent { userId = request.userResearchDto.UserId, researchId = request.userResearchDto.ResearchId });
                return ApiResponse<Unit>.Success(Unit.Value, 201);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure(ex.Message, 404);
            }
            catch (EntityAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<Unit>.Failure("Something went wrong", 500);

            }
        }
    }
}
