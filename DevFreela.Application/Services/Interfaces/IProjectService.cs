using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {

        List<Project> GetAll(string query);

        Project GetById(int id);

        Project Create(Project entity);

        Project Update(Project entity);

        void Delete(int id);

        void Start(int id);

        void Finish(int id);

        ProjectComment CreatedComment(ProjectComment entity);

    }
}