using Domain.Models.LabTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Queries.LabTestingQueries
{
    public interface ILabTestResultQueryRepository
    {
        Task<LabTestResult> GetLabTestResultByIdAsync(Guid labTestResultId);
    }   
}
