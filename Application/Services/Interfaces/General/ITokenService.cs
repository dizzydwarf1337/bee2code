using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.General
{
    public interface ITokenService
    {
        Task<string> GetLoginToken(Guid userId);
        Task<bool> CheckUserToken(Guid userId, string token);
        Task DeleteUserToken(Guid userId);
    }
}
