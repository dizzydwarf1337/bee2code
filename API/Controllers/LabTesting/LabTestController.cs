using Application.DTO.LabTesting;
using Application.Features.LabTests.Commands.CreateLabTest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LabTesting
{
    public class LabTestController : BaseController
    {
        [Authorize(Roles = "Admin,Worker")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLabTest(CreateLabTestDto createLabTestDto)
        {
            return HandleResponse(await Mediator.Send(new CreateLabTestCommand { createLabTestDto = createLabTestDto }));
        }
    }
}
