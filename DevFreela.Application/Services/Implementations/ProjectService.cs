
using DevFreela.Application.InputModel;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModel;
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

        public CreatedProjectViewModel Create(NewProjectInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public UpdatedProjectViewModel Update(int id, UpdateProjectInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public CreatedComment CreatedComment(int id, CreateCommentInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Finish(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            throw new NotImplementedException();
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Start(int id)
        {
            throw new NotImplementedException();
        }
    }

}