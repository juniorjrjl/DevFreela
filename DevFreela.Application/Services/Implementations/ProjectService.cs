
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    
    public class ProjectService : IProjectService
    {

        private readonly DevFreelaDbContext _dbContext; 

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Project Create(Project entity)
        {
            _dbContext.Projects.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Project Update(Project entity)
        {
            var toUpdate = GetById(entity.Id);
            toUpdate.Title = entity.Title;
            toUpdate.Description = entity.Description;
            toUpdate.TotalCost = entity.TotalCost;
            _dbContext.SaveChanges();
            return entity;
        }

        public ProjectComment CreatedComment(ProjectComment entity)
        {
            _dbContext.ProjectsComments.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var project = GetById(id);
            project.Cancel();
            _dbContext.SaveChanges();
        }

        public void Finish(int id)
        {
            var project = GetById(id);
            project.Finish();
            _dbContext.SaveChanges();
        }

        public List<Project> GetAll(string query) => _dbContext.Projects.ToList();

        public Project GetById(int id)
        {
            try
            {
                return _dbContext.Projects.Single(p => p.Id == id);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Projeto {id} não encontrado", ex);
            }
        }

        public void Start(int id)
        {
            var project = GetById(id);
            project.Start();
            _dbContext.SaveChanges();
        }

    }

}
