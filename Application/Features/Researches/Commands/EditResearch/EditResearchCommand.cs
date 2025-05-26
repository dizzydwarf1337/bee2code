using Application.Core.ApiResponse;
using Application.DTO.Researches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Commands.EditResearch
{
    public class EditResearchCommand : IRequest<ApiResponse<ResearchDto>>
    {
        public EditResearchDto EditResearchDto { get; set; } = default!;
    }
}
