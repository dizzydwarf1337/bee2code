using Domain.Models.LabTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Commands.LabTestingCommands
{
    public interface ILabTestResultCommandRepository
    {
        Task<LabTestResult> CreateLabTestResultAsync(LabTestResult labTestResult);
        Task<LabTestResult> UpdateLabTestResultAsync(LabTestResult labTestResult);
        Task DeleteLabTestResultAsync(Guid labTestResultId);
    }
}
