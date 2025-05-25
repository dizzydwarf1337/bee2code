using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using Application.Features.LabTests.Events.LabTestCreated;
using Application.Services.Interfaces.LabTesting;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Commands.CreateLabTest
{
    public class CreateLabTestCommandHandler : IRequestHandler<CreateLabTestCommand, ApiResponse<LabTestDto>>
    {
        private readonly ILabTestService _labTestService;
        private readonly ILogger<CreateLabTestCommandHandler> _logger;
        private readonly IMediator _mediator;
        public CreateLabTestCommandHandler(ILabTestService labTestService, 
            ILogger<CreateLabTestCommandHandler> logger,
            IMediator mediator)
        {
            _labTestService = labTestService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ApiResponse<LabTestDto>> Handle(CreateLabTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var labTest = await _labTestService.CreateLabTestAsync(request.createLabTestDto);
                await _mediator.Publish(new LabTestCreatedEvent { patientId = request.createLabTestDto.PatientId, researchId = request.createLabTestDto.ResearchId });
                return ApiResponse<LabTestDto>.Success(labTest,201);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestDto>.Failure(ex.Message, 404);
            }
            catch (EntityAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestDto>.Failure(ex.Message, 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestDto>.Failure("Something went wrong", 500);
            }
        }
    }
}
