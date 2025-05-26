using Domain.Models.Researches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Queries.ResearchesQueries
{
    public interface IResearchQueryRepository
    {
        Task<ICollection<Research>> GetResearchesPaginatedAsync(int page, int pageSize);
        Task<ICollection<Research>> GetResearchesByUserIdAsync(Guid userId); // User reseaches
        Task<Research> GetResearchByIdAsync(Guid reseachId, Guid? userId, string? userRole = "Patient");
        Task<ICollection<Research>> GetResearchesByOwnerIdAsync(Guid ownerId);
    }
}
