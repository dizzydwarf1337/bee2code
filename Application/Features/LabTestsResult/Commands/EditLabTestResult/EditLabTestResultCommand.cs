using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Commands.EditLabTestResult
{
    public class EditLabTestResultCommand : IRequest<ApiResponse<LabTestResultDto>>
    {
        public EditLabTestResultDto EditLabTestResultDto { get; set; } = default!;
    }
}
