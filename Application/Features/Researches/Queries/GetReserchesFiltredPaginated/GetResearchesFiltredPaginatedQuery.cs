using Application.Core.ApiResponse;
using Application.DTO.Researches;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Queries.GetReserchesFiltredPaginated
{
    public class GetResearchesFiltredPaginatedQuery : IRequest<ApiResponse<List<ResearchPreviewDto>>>
    {
        public string? ownerId { get; set; }
        public string? patientId { get; set; }
        [Range(1,50)]
        public int PageSize { get; set; }
        [Range(1,int.MaxValue)]
        public int Page { get; set; }
    }
}
