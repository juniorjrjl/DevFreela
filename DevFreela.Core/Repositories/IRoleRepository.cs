using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IRoleRepository
{
    Task<Role> AddAsync(Role project);

}