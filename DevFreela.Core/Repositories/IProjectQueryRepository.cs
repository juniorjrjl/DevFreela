using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.model;

namespace DevFreela.Core.Repositories
{
    public interface IProjectQueryRepository
    {

        Task<PaginationResult<Project>> GetAllAsync(string? query, int page = 1);

        Task<Project> GetByIdAsync(int id);

    }
}
