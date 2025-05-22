using Domain.Models.LabTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Commands.LabTestingCommands
{
    public interface ILabTestCommandRepository
    {
        Task<LabTest> CreateLabTestAsync(LabTest labTest);
        Task<LabTest> UpdateLabTestAsync(LabTest labTest);
        Task DeleteLabTestAsync(Guid labTestId);
    }
}
