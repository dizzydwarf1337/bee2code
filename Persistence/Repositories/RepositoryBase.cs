using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RepositoryBase
    {
        internal readonly BeeCodeDbContext _context;
        public RepositoryBase(BeeCodeDbContext context)
        {
            _context = context;
        }
    }
}
