using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using Application.Services.Interfaces.LabTesting;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Queries.GetLabTestResultById
{
    public class GetLabTestReultByIdQueryHandler : IRequestHandler<GetLabTestResultByIdQuery, ApiResponse<LabTestResultDto>>
    {
        private readonly ILogger<GetLabTestReultByIdQueryHandler> _logger;
        private readonly ILabTestResultService _labTestResultService;

        public GetLabTestReultByIdQueryHandler(ILogger<GetLabTestReultByIdQueryHandler> logger, ILabTestResultService labTestResultService)
        {
            _logger = logger;
            _labTestResultService = labTestResultService;
        }

        public async Task<ApiResponse<LabTestResultDto>> Handle(GetLabTestResultByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return ApiResponse<LabTestResultDto>.Success(await _labTestResultService.GetLabTestResultByIdAsync(Guid.Parse(request.labTestResultId)));
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestResultDto>.Failure(ex.Message, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestResultDto>.Failure("An error occurred while processing your request.", 500);

            }
        }
    }
}
