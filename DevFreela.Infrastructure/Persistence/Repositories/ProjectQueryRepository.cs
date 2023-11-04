using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectQueryRepository : IProjectQueryRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectQueryRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllAsync(string? query) 
        {
            
            IQueryable<Project> projects = _dbContext.Projects;
            if (!string.IsNullOrWhiteSpace(query))
            {
                projects = projects.Where(p => p.Title.Contains(query) || p.Description.Contains(query));
            }
            return await projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Projects.SingleAsync(p => p.Id == id);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Projeto {id} não encontrado", ex);
            }
        }

    }
}
