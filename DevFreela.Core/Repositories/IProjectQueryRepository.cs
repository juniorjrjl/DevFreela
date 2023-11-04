using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectQueryRepository
    {

        Task<List<Project>> GetAllAsync(string? query);

        Task<Project> GetByIdAsync(int id);

    }
}
