using Application.Core.ApiResponse;
using Application.DTO.Researches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Commands.CreateResearch
{
    public class CreateResearchCommand : IRequest<ApiResponse<ResearchDto>>
    {
        public CreateResearchDto createResearchDto { get; set; }
    }
}
