
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillQueryRepository
    {
        Task<List<Skill>> GetAllAsync();
    }
}
