using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.model;

namespace DevFreela.API.Mappers;

public class ProjetcMapper : IProjetcMapper
{
    public CreateProjectCommand ToInputModel(NewProjectInputModel inputModel) => 
        new 
        (
            inputModel.Title, 
            inputModel.Description, 
            inputModel.ClientId, 
            inputModel.FreelancerId, 
            inputModel.TotalCost
        );

    public UpdateProjectCommand ToCommand(UpdateProjectInputModel inputModel, int id) => 
        new 
        (
            inputModel.Title,
            inputModel.Description,
            inputModel.TotalCost,
            id
        );

    public CreateProjectCommentCommand ToCommand(CreateCommentInputModel inputModel, int ProjectId) => 
    new 
    (
        inputModel.Comment, 
        inputModel.UserId, 
        ProjectId
    );

    public CreatedProjectViewModel ToCreatedViewModel(Project entity) => 
        new 
        (
            entity.Id,
            entity.Title,
            entity.Description,
            entity.ClientId,
            entity.FreelancerId,
            entity.TotalCost
        );

    public ProjectDetailsViewModel ToDetailsViewModel(Project entity) => 
    new 
    (
        entity.Id,
        entity.Title,
        entity.Description,
        entity.TotalCost,
        entity.CreatedAt,
        entity.FinishedAt
    );

    public PaginationResult<ProjectViewModel> ToPagedViewModel(PaginationResult<Project> entity) => 
        new 
        (
            entity.Page,
            entity.TotalPages,
            entity.PageSize,
            entity.ItemCount,
            entity.Data.Select(p => ToListViewMode(p)).ToList()
        );

    public UpdatedProjectViewModel ToViewModel(Project entity) => 
        new 
        (
            entity.Id,
            entity.Title,
            entity.Description,
            entity.TotalCost
        );

    private static ProjectViewModel ToListViewMode(Project entity) => new (entity.Id, entity.Title, entity.CreatedAt);
}