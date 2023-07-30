using DevFreela.Application.InputModel;
using DevFreela.Application.ViewModel;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {

        List<ProjectViewModel> GetAll(string query);

        ProjectDetailsViewModel GetById(int id);

        CreatedProjectViewModel Create(NewProjectInputModel inputModel);

        UpdatedProjectViewModel Update(int id, UpdateProjectInputModel inputModel);

        void Delete(int id);

        void Start(int id);

        void Finish(int id);

        CreatedComment CreatedComment(int id, CreateCommentInputModel inputModel);

    }
}