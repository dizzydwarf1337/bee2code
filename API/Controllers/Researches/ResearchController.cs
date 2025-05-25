using Application.DTO.Researches;
using Application.Features.Researches.Commands.AddUserToResearch;
using Application.Features.Researches.Commands.CreateResearch;
using Application.Features.Researches.Commands.RemoveUserFromResearch;
using Application.Features.Researches.Queries.DownloadUserAcceptance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Researches
{
    public class ResearchController : BaseController
    {
        [Authorize(Roles = "Admin,Worker")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateResearch([FromBody] CreateResearchDto createResearchDto)
        {
            return HandleResponse(await Mediator.Send(new CreateResearchCommand { createResearchDto = createResearchDto }));
        }
        [Authorize(Roles = "Admin,Worker")]
        [HttpPost("addUser")]
        public async Task<IActionResult> AddUserToResearch([FromForm] CreateUserResearchDto userResearchDto)
        {
            return HandleResponse(await Mediator.Send(new AddUserToResearchCommand { userResearchDto = userResearchDto }));
        }
        [Authorize(Roles = "Admin,Worker")]
        [HttpDelete("delete/userResearch/{userId}/{researchId}")]
        public async Task<IActionResult> RemoveUserFromResearch([FromRoute]string userId, [FromRoute]string researchId)
        {
            return HandleResponse(await Mediator.Send(new RemoveUserResearchCommand { RemoveUserResearchDto = new RemoveUserResearchDto { UserId = userId, ResearchId = researchId } }));
        }
        [Authorize]
        [HttpGet("downloadAcceptance/{userId}/{researchId}")]
        public async Task<IActionResult> DownloadUserAccpetance([FromRoute]string userId, [FromRoute]string researchId)
        {
            var result = await Mediator.Send(new DownloadUserAcceptanceQuery { researchId = researchId, userId = userId });
            Console.WriteLine(result.ContentType);
            Console.WriteLine(result.FileName);
            if (result.Content != null) return File(result.Content, result.ContentType, result.FileName);
            else return NotFound();
        }
    }
}
