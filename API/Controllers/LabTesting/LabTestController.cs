using Application.DTO.LabTesting;
using Application.Features.LabTests.Commands.CreateLabTest;
using Application.Features.LabTests.Commands.DeleteLabTest;
using Application.Features.LabTests.Commands.EditLabTest;
using Application.Features.LabTests.Queries.GetLabTestById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

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
        [Authorize]
        [HttpGet("{labTestId}")]
        public async Task<IActionResult> GetLabTestById([FromRoute] string labTestId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return HandleResponse(await Mediator.Send(new GetLabTestByIdQuery { labTestId = labTestId, userId = userId, userRole = role }));
        }
    }
}
