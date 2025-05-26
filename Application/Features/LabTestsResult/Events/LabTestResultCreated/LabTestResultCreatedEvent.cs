using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTestsResult.Events.LabTestResultCreated
{
    public class LabTestResultCreatedEvent : INotification
    {
        public string labTestId { get; set; }
    }
}
