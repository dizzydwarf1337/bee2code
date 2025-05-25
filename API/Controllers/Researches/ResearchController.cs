using Application.DTO.Researches;
using Application.Features.Researches.Commands.AddUserToResearch;
using Application.Features.Researches.Commands.CreateResearch;
using Application.Features.Researches.Commands.RemoveUserFromResearch;
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
    }
}
