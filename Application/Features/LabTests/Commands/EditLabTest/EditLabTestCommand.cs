using Application.Core.ApiResponse;
using Application.DTO.LabTesting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Commands.EditLabTest
{
    public class EditLabTestCommand : IRequest<ApiResponse<LabTestDto>>
    {
        public EditLabTestDto EditLabTestDto { get; set; } = default!;
    }
}
