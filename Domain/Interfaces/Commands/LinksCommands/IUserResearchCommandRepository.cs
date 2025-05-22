using Domain.Models.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Commands.LinksCommands
{
    public interface IUserResearchCommandRepository
    {
        Task<UserResearch> CreateUserResearchAsync(UserResearch userResearch);
        Task DeleteUserResearchAsync(Guid researchId);
    }
}
