using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Core.Repositories;

public interface IRoleQueryRepository
{

    Task<Role> GetByNameAsync(RoleNameEnum name);
    
}
