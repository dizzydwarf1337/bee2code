using Application.DTO.Researches;
using Application.Features.Researches.Commands.AddUserToResearch;
using Application.Features.Researches.Commands.CreateResearch;
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
        public async Task<IActionResult> AddUserToResearch([FromForm] UserResearchDto userResearchDto)
        {
            return HandleResponse(await Mediator.Send(new AddUserToResearchCommand { userResearchDto = userResearchDto }));
        }
    }
}
