using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using Application.Services.Interfaces.LabTesting;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Commands.EditLabTestResult
{
    public class EditLabTestResultCommandHandler : IRequestHandler<EditLabTestResultCommand, ApiResponse<LabTestResultDto>>
    {
        private readonly ILabTestResultService _labTestResultService;
        private readonly ILogger<EditLabTestResultCommandHandler> _logger;

        public EditLabTestResultCommandHandler(ILabTestResultService labTestResultService, ILogger<EditLabTestResultCommandHandler> logger)
        {
            _labTestResultService = labTestResultService;
            _logger = logger;
        }

        public async Task<ApiResponse<LabTestResultDto>> Handle(EditLabTestResultCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedResult = await _labTestResultService.UpdateLabTestResultAsync(request.EditLabTestResultDto);
                _logger.LogInformation($"LabTestResult was updated:  {request.EditLabTestResultDto.Id}");
                return ApiResponse<LabTestResultDto>.Success(updatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting labtestResult error");
                return ApiResponse<LabTestResultDto>.Failure("Internal error", 500);
            }
        }
    }
}
