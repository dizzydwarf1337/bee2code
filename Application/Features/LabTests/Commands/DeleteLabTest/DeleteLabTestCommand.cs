using Application.Core.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Commands.DeleteLabTest
{
    public class DeleteLabTestCommand : IRequest<ApiResponse<Unit>>
    {
        public string LabTestId { get; set; } = default!;
    }
}
