using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class RoleQueryRepository : IRoleQueryRepository
    {

        private readonly DevFreelaDbContext _dbContext;

        public RoleQueryRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> GetByNameAsync(RoleNameEnum name)
        {
            try
            {
                return await _dbContext.Roles.SingleAsync(p => p.Name == name);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"A Role {name} não foi encontrada", ex);
            }
        }

    }
}
