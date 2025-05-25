using Application.DTO.Researches;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Queries.DownloadUserAcceptance
{
    public class DownloadUserAcceptanceQuery : IRequest<AcceptanceDownloadDto>
    {
        public string userId { get; set; }
        public string researchId { get; set; }
    }
}
