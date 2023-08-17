using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectQueryRepository
    {

        Task<List<Project>> GetAllAsync();

        Task<Project> GetByIdAsync(int id);

    }
}
