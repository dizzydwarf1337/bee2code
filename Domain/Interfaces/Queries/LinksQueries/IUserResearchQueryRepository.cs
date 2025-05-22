using Domain.Models.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Queries.LinksQueries
{
    public interface IUserResearchQueryRepository
    {
        Task<ICollection<UserResearch>> GetUserResearchesPaginatedAsync(int page, int pageSize);
        Task<UserResearch> GetUserResearchByIdAsync(Guid userResearchId);
    }
}
