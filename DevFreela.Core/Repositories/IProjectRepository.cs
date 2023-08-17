using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);

        Task<ProjectComment> AddCommentAsync(ProjectComment projectComment);

        Task<Project> UpdateAsync(Project project);

    }
}
