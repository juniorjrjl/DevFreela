using DevFreela.Core.Entities;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Persistence.model;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectQueryRepository : IProjectQueryRepository
{
    private const int PAGE_SIZE = 2;
    private readonly DevFreelaDbContext _dbContext;

    public ProjectQueryRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Project>> GetAllAsync(string? query, int page) 
    {
        
        IQueryable<Project> projects = _dbContext.Projects;
        if (!string.IsNullOrWhiteSpace(query))
        {
            projects = projects.Where(p => p.Title.Contains(query) || p.Description.Contains(query));
        }
        return await projects.GetPaged(page, PAGE_SIZE);
    }

    public async Task<Project> GetByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Projects.SingleAsync(p => p.Id == id);
        }
        catch (InvalidOperationException ex)
        {
            throw new NotFoundException($"Projeto {id} não encontrado", ex);
        }
    }

}
