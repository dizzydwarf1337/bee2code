using Application.Core.ApiResponse;
using Application.Features.LabTests.Events.LabTestDeleted;
using Application.Services.Interfaces.LabTesting;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using MediatR.Wrappers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Commands.DeleteLabTest
{
    public class DeleteLabTestCommandHandler : IRequestHandler<DeleteLabTestCommand, ApiResponse<Unit>>
    {
        private readonly ILabTestService _labService;
        private readonly ILogger<DeleteLabTestCommandHandler> _logger;
        private readonly IMediator _mediator;

        public DeleteLabTestCommandHandler(ILabTestService labService, ILogger<DeleteLabTestCommandHandler> logger, IMediator mediator)
        {
            _labService = labService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ApiResponse<Unit>> Handle(DeleteLabTestCommand request, CancellationToken cancellationToken)
        {
            try
            {

                await _mediator.Publish(new LabTestDeletedEvent { LabTestId = request.LabTestId });
                await _labService.DeleteLabTestAsync(Guid.Parse(request.LabTestId));
                _logger.LogInformation($"Lab test with ID {request.LabTestId} deleted successfully.");
                return ApiResponse<Unit>.Success(Unit.Value);
            }
            catch(EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Lab test with ID {request.LabTestId} not found for deletion.");
                return ApiResponse<Unit>.Failure($"Lab test with ID {request.LabTestId} not found.", 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the lab test with ID {request.LabTestId}");
                return  ApiResponse<Unit>.Failure("An error occurred while deleting the lab test.",500);
            }
        }
    }
}
