using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class RoleRepository(DevFreelaDbContext dbContext) : IRoleRepository
{

    private readonly DevFreelaDbContext _dbContext = dbContext;

    public async Task<Role> AddAsync(Role role)
    {
        await _dbContext.Roles.AddAsync(role);
        return role;
    }

}
