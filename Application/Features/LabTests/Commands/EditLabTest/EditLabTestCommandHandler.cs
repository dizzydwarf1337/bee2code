using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using Application.Services.Interfaces.LabTesting;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Commands.EditLabTest
{
    public class EditLabTestCommandHandler : IRequestHandler<EditLabTestCommand, ApiResponse<LabTestDto>>
    {
        private readonly ILabTestService _labTestService;
        private readonly ILogger<EditLabTestCommandHandler> _logger;

        public EditLabTestCommandHandler(ILabTestService labTestService, ILogger<EditLabTestCommandHandler> logger)
        {
            _labTestService = labTestService;
            _logger = logger;
        }

        public async Task<ApiResponse<LabTestDto>> Handle(EditLabTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var labTestDto = await _labTestService.UpdateLabTestAsync(request.EditLabTestDto);
                return ApiResponse<LabTestDto>.Success(labTestDto);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex, "Lab test not found for editing: {LabTestId}", request.EditLabTestDto.Id);
                return ApiResponse<LabTestDto>.Failure($"Lab test with ID {request.EditLabTestDto.Id} not found.",404);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while editing the lab test.");
                return ApiResponse<LabTestDto>.Failure("An error occurred while editing the lab test.");
            }
        }
    }
}
