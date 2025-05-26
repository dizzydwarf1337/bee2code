using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LabTests.Events.LabTestDeleted
{
    public class LabTestDeletedEvent : INotification
    {
        public string LabTestId { get; set; } = default!;
    }
}
