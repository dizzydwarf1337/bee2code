using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using Application.Features.LabTests.Events.LabTestCreated;
using Application.Features.LabTestsResult.Events.LabTestResultCreated;
using Application.Services.Interfaces.LabTesting;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Commands.CreateLabTestResult
{
    public class CreateLabTestResultCommandHandler : IRequestHandler<CreateLabTestResultCommand,ApiResponse<LabTestResultDto>>
    {
        private readonly ILabTestResultService _labTestResultService;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateLabTestResultCommandHandler> _logger;

        public CreateLabTestResultCommandHandler(ILabTestResultService labTestResultService,
            IMediator mediator,
            ILogger<CreateLabTestResultCommandHandler> logger)
        {
            _labTestResultService = labTestResultService;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<ApiResponse<LabTestResultDto>> Handle(CreateLabTestResultCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = await _labTestResultService.CreateLabTestResultAsync(request.createLabTestResultDto);
                await _mediator.Publish(new LabTestResultCreatedEvent { labTestId = dto.LabTestId });
                return ApiResponse<LabTestResultDto>.Success(dto, 201);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestResultDto>.Failure(ex.Message, 404);
            }
            catch (EntityCreatingException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestResultDto>.Failure(ex.Message);
            }
            catch (EntityAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestResultDto>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestResultDto>.Failure("Internal error",500);
            }
        }
    }
}
