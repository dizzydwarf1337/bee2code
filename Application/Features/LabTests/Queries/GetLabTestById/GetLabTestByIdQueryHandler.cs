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

namespace Application.Features.LabTests.Queries.GetLabTestById
{
    public class GetLabTestByIdQueryHandler : IRequestHandler<GetLabTestByIdQuery, ApiResponse<LabTestDto>>
    {
        private readonly ILabTestService _labTestService;
        private readonly ILogger<GetLabTestByIdQueryHandler> _logger;

        public GetLabTestByIdQueryHandler(ILabTestService labTestService, ILogger<GetLabTestByIdQueryHandler> logger)
        {
            _labTestService = labTestService;
            _logger = logger;
        }

        public async Task<ApiResponse<LabTestDto>> Handle(GetLabTestByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return ApiResponse<LabTestDto>.Success(await _labTestService.GetLabTestByIdAsync(Guid.Parse(request.labTestId), Guid.Parse(request.userId), request.userRole));
            }
            catch(EntityNotFoundException ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestDto>.Failure("Lab test doesn't exists or you have no permission it",404);
            } 
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return ApiResponse<LabTestDto>.Failure("Internal error", 500);
            }
        }
    }
}
