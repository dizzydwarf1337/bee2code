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
        Task<ICollection<LabTestResult>> GetLabTestResultsPaginatedAsync(int page, int pageSize);
        Task<LabTestResult> GetLabTestResultByIdAsync(Guid labTestResultId);
        Task<ICollection<LabTestResult>> GetLabTestResultsByUserIdAsync(Guid userId);
        Task<ICollection<LabTestResult>> GetLabTestResultsByLabTestIdAsync(Guid labTestId);
    }
}
