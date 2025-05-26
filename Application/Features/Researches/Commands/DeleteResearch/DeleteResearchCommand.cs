using Application.Core.ApiResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Commands.DeleteResearch
{
    public class DeleteResearchCommand : IRequest<ApiResponse<Unit>>
    {
        public string researchId { get; set; } = default!;
    }
}
