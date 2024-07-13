using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{

    private readonly DevFreelaDbContext _dbContext;

    public RoleRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role> AddAsync(Role role)
    {
        await _dbContext.Roles.AddAsync(role);
        return role;
    }

}
