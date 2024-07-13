using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Project> AddAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        return project;
    }

    public async Task<Project> UpdateAsync(Project project) => await Task.Run(() => project);

    public async Task<ProjectComment> AddCommentAsync(ProjectComment projectComment)
    {
        await _dbContext.ProjectsComments.AddAsync(projectComment);
        return projectComment;
    }
}
