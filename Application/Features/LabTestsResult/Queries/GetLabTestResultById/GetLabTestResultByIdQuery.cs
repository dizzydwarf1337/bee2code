using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Queries.GetLabTestResultById
{
    public class GetLabTestResultByIdQuery : IRequest<ApiResponse<LabTestResultDto>>
    {
        public string labTestResultId { get; set; }
    }
}
