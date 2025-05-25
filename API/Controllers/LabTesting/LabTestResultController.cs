using Application.DTO.LabTesting;
using Application.Features.LabTestsResult.Commands.CreateLabTestResult;
using Application.Features.LabTestsResult.Commands.DeleteLabTestResult;
using Application.Features.LabTestsResult.Commands.EditLabTestResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LabTesting
{
    public class LabTestResultController : BaseController
    {
        [Authorize(Roles="Admin,Worker")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateLabTestResult(CreateLabTestResultDto createLabTestResultDto)
        {
            return HandleResponse(await Mediator.Send(new CreateLabTestResultCommand { createLabTestResultDto =  createLabTestResultDto }));
        }
        [Authorize(Roles ="Admin,Worker")]
        [HttpDelete("delete/{deleteLabTestResultId}")]
        public async Task<IActionResult> DeleteLabTestResult([FromRoute]string deleteLabTestResultId)
        {
            return HandleResponse(await Mediator.Send(new DeleteLabTestResultCommand {LabTestResultId = deleteLabTestResultId}));
        }
        [Authorize(Roles ="Admin,Worker")]
        [HttpPut("update")]
        public async Task<IActionResult> EditLabTestResult([FromBody]EditLabTestResultDto editLabTestResultDto)
        {
            return HandleResponse(await Mediator.Send(new EditLabTestResultCommand { EditLabTestResultDto = editLabTestResultDto }));
        }
    }
}
