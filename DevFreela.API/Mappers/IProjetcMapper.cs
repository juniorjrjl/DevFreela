using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.model;

namespace DevFreela.API.Mappers;

public interface IProjetcMapper
{
    
    PaginationResult<ProjectViewModel> ToPagedViewModel(PaginationResult<Project> entity);

    ProjectDetailsViewModel ToDetailsViewModel(Project entity);

    CreateProjectCommand ToInputModel(NewProjectInputModel inputModel);

    CreatedProjectViewModel ToCreatedViewModel(Project entity);

    UpdateProjectCommand ToCommand(UpdateProjectInputModel inputModel, int id);

    UpdatedProjectViewModel ToViewModel(Project entity);

    CreateProjectCommentCommand ToCommand(CreateCommentInputModel inputModel, int ProjectId);

}
