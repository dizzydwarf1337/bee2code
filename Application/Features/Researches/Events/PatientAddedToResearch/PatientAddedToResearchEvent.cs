using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Researches.Events.PatientAddedToResearch
{
    public class PatientAddedToResearchEvent : INotification
    {
        public string userId { get; set; }
        public string researchId { get; set; }
    }
}
