using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectQueryRepository
    {

        Task<ICollection<Project>> GetAllAsync();

        Task<Project> GetByIdAsync(int id);

    }
}
