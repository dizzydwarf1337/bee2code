using Application.Core.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Commands.DeleteLabTestResult
{
    public class DeleteLabTestResultCommand : IRequest<ApiResponse<Unit>>
    {
        public string LabTestResultId { get; set; } = default!;
    }
}
