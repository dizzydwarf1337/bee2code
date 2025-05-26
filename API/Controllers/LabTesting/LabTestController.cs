using Application.DTO.LabTesting;
using Application.Features.LabTests.Commands.CreateLabTest;
using Application.Features.LabTests.Commands.DeleteLabTest;
using Application.Features.LabTests.Commands.EditLabTest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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
        [Authorize(Roles="Admin,Worker")]
        [HttpDelete("delete/{deleteLabTestId}")]
        public async Task<IActionResult> DeleteLabTest([FromRoute]string deleteLabTestId)
        {
            return HandleResponse(await Mediator.Send(new DeleteLabTestCommand { LabTestId = deleteLabTestId}));
        }
        [Authorize(Roles ="Admin,Worker")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateLabTest([FromBody]EditLabTestDto editLabTestDto)
        {
            return HandleResponse(await Mediator.Send(new EditLabTestCommand { EditLabTestDto = editLabTestDto }));
        }
    }
}
