using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Commands.CreateLabTestResult
{
    public class CreateLabTestResultCommand : IRequest<ApiResponse<LabTestResultDto>>
    {
        public CreateLabTestResultDto createLabTestResultDto { get; set; }
    }
}
