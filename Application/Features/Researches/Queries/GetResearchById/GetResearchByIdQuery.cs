using Application.Core.ApiResponse;
using Application.DTO.Researches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Queries.GetResearchById
{
    public class GetResearchByIdQuery : IRequest<ApiResponse<ResearchDto>>
    {
        public string researchId { get; set; }
        public string userRole { get; set; }
        public string userId { get; set; }
    }
}
