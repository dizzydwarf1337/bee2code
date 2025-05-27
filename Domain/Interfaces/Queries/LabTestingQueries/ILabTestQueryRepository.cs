using Domain.Models.LabTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Queries.LabTestingQueries
{
    public interface ILabTestQueryRepository
    {
        Task<ICollection<LabTest>> GetLabTestsPaginatedAsync(int page, int pageSize);
        Task<LabTest> GetLabTestByIdAsync(Guid labTestId, Guid? userId, string? UserRole = "Patient");
        Task<ICollection<LabTest>> GetLabTestsByUserIdAsync(Guid userId);
        Task<ICollection<LabTest>> GetLabTestsByResearchIdAsync(Guid researchId);
        Task<ICollection<LabTest>> GetLabTestsByCreatorIdAsync(Guid creatorId);

    }
}
