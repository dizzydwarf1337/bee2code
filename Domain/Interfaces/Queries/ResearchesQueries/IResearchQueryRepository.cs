using Domain.Models.Researches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Queries.ResearchesQueries
{
    public interface IResearchQueryRepository
    {
        Task<ICollection<Research>> GetResearchesByPatientIdPaginatedAsync(Guid userId, int Page, int PageSize); // Patient reseaches
        Task<Research> GetResearchByIdAsync(Guid reseachId, Guid? userId, string? userRole = "Patient");
        Task<ICollection<Research>> GetResearchesByOwnerIdPaginatedAsync(Guid ownerId, int Page, int PageSize);
        Task<ICollection<Research>> GetResearchesFiltredPaginated(Guid? ownerId, Guid? patientId, int? Page, int? PageSize);
    }
}
