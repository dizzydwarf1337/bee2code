using Application.Core.ApiResponse;
using Application.DTO.Researches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Queries.GetPatientResearchesPaginated
{
    public class GetPatientResearchesPaginatedQuery : IRequest<ApiResponse<List<ResearchDto>>>
    {
        public string patientId { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
