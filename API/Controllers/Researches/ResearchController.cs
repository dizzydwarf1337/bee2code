using Application.DTO.Researches;
using Application.Features.Researches.Commands.AddUserToResearch;
using Application.Features.Researches.Commands.CreateResearch;
using Application.Features.Researches.Commands.DeleteResearch;
using Application.Features.Researches.Commands.EditResearch;
using Application.Features.Researches.Commands.RemoveUserFromResearch;
using Application.Features.Researches.Queries.DownloadUserAcceptance;
using Application.Features.Researches.Queries.GetPatientResearchesPaginated;
using Application.Features.Researches.Queries.GetResearchById;
using Application.Features.Researches.Queries.GetReserchesFiltredPaginated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

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
        [HttpDelete("delete/userResearch/{userId}/{deleteResearchId}")]
        public async Task<IActionResult> RemoveUserFromResearch([FromRoute]string userId, [FromRoute]string deleteResearchId)
        {
            return HandleResponse(await Mediator.Send(new RemoveUserResearchCommand { RemoveUserResearchDto = new RemoveUserResearchDto { UserId = userId, ResearchId = deleteResearchId } }));
        }
        [Authorize]
        [HttpGet("downloadAcceptance/{userId}/{researchId}")]
        public async Task<IActionResult> DownloadUserAccpetance([FromRoute]string userId, [FromRoute]string researchId)
        {
            var result = await Mediator.Send(new DownloadUserAcceptanceQuery { researchId = researchId, userId = userId });
            if (result.Content != null) return File(result.Content, result.ContentType, result.FileName);
            else return NotFound();
        }
        [Authorize(Roles ="Admin,Worker")]
        [HttpPut("update")]
        public async Task<IActionResult> EditResearch([FromBody] EditResearchDto editResearchDto)
        {
            return HandleResponse(await Mediator.Send(new EditResearchCommand { EditResearchDto = editResearchDto }));
        }
        [Authorize(Roles ="Admin,Worker")]
        [HttpDelete("delete/{deleteResearchId}")]
        public async Task<IActionResult> DeleteResearch([FromRoute]string deleteResearchId)
        {
            return HandleResponse(await Mediator.Send(new DeleteResearchCommand { researchId= deleteResearchId }));
        }
        [Authorize]
        [HttpGet("{researchId}")]
        public async Task<IActionResult> GetResearchById([FromRoute]string researchId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return HandleResponse(await Mediator.Send(new GetResearchByIdQuery { researchId = researchId,userId = userId,userRole=role}));
        }
        [Authorize(Roles = "Admin,Worker")]
        [HttpGet("filter")]
        public async Task<IActionResult> GetResarchesFiltredPaginated([FromQuery] string? ownerId, [FromQuery] string? patientId, [FromQuery] int page, [FromQuery] int PageSize)
        {
            return HandleResponse(await Mediator.Send(new GetResearchesFiltredPaginatedQuery { ownerId = ownerId, patientId = patientId, Page =page, PageSize =PageSize }));
        }
        [Authorize]
        [HttpGet("user/{getResearchesByPatientId}")]
        public async Task<IActionResult> GetPatientResearchesPaginated([FromRoute]string getResearchesByPatientId, [FromQuery] int page, [FromQuery]int pageSize)
        {
            return HandleResponse(await Mediator.Send(new GetPatientResearchesPaginatedQuery { patientId = getResearchesByPatientId, page = page, pageSize = pageSize }));
        }

    }
}
