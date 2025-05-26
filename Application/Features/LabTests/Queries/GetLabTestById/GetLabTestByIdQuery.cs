using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Queries.GetLabTestById
{
    public class GetLabTestByIdQuery : IRequest<ApiResponse<LabTestDto>>
    {
        public string labTestId { get; set; }
        public string userRole {  get; set; }
        public string userId { get; set; }
    }
}
