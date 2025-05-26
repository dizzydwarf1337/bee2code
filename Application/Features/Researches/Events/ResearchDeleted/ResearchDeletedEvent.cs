using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Events.ResearchDeleted
{
    public class ResearchDeletedEvent : INotification
    {
        public string researchId { get; set; } = default!;
    }
}
