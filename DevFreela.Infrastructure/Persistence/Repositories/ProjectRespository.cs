using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IProjectQueryRepository _projectQueryRepository;

        public ProjectRepository(DevFreelaDbContext dbContext, IProjectQueryRepository projectQueryRepository)
        {
            _dbContext = dbContext;
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task<Project> AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            return project;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            await _dbContext.SaveChangesAsync();
            return project;
        }

        public async Task<ProjectComment> AddCommentAsync(ProjectComment projectComment)
        {
            await _dbContext.ProjectsComments.AddAsync(projectComment);
            await _dbContext.SaveChangesAsync();
            return projectComment;
        }
    }
}
