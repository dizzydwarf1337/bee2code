using Application.DTO.LabTesting;
using Application.Features.LabTestsResult.Commands.CreateLabTestResult;
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
    }
}
