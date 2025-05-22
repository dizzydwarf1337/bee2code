using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Queries.UserQueries
{
    public interface IUserQueryRepository
    {
        Task<ICollection<User>> GetAllUsersPaginatedAsync(int page, int pageSize);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<ICollection<User>> GetUsersByResearchIdAsync(Guid researchId);
    }
}
