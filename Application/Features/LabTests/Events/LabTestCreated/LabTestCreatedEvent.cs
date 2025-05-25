using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Events.LabTestCreated
{
    public class LabTestCreatedEvent : INotification
    {
        public string researchId { get; set; }
        public string patientId { get; set; }
    }
}
