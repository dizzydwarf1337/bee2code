using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Commands.CreateLabTest
{
    public class CreateLabTestCommand : IRequest<ApiResponse<LabTestDto>>
    {
        public CreateLabTestDto createLabTestDto { get; set; }
    }
}
