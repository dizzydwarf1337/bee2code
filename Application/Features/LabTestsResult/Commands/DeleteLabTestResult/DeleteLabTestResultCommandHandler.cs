using Application.Core.ApiResponse;
using Application.Services.Interfaces.LabTesting;
using Domain.Exceptions.BusinessExceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Commands.DeleteLabTestResult
{
    public class DeleteLabTestResultCommandHandler : IRequestHandler<DeleteLabTestResultCommand,ApiResponse<Unit>>
    {
        private readonly ILabTestResultService _labTestResultService;
        private readonly ILogger<DeleteLabTestResultCommandHandler> _logger;

        public DeleteLabTestResultCommandHandler(ILabTestResultService labTestResultService, ILogger<DeleteLabTestResultCommandHandler> logger)
        {
            _labTestResultService = labTestResultService;
            _logger = logger;
        }

        public async Task<ApiResponse<Unit>> Handle(DeleteLabTestResultCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _labTestResultService.DeleteLabTestResultAsync(Guid.Parse(request.LabTestResultId));
                return ApiResponse<Unit>.Success(Unit.Value);
            }
            catch(EntityNotFoundException ex)
            {
                _logger.LogError(ex.ToString());
                return ApiResponse<Unit>.Failure(ex.Message, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting LabTestResult");
                return ApiResponse<Unit>.Failure("Internal error", 500);
            }
        }
    }
}
