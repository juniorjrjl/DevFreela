using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.model;

namespace DevFreela.API.Mappers
{
    public class ProjectProfile : Profile
    {
        
        public ProjectProfile()
        {
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Project, ProjectDetailsViewModel>();
            CreateMap<NewProjectInputModel, CreateProjectCommand>();
            CreateMap<Project, CreatedProjectViewModel>();
            CreateMap<UpdateProjectInputModel, UpdateProjectCommand>()
                .ConstructUsing(src => new UpdateProjectCommand(src.Title, src.Description, src.TotalCost, null));
            CreateMap<Project, UpdatedProjectViewModel>();
            CreateMap<CreateCommentInputModel, ProjectComment>();
            CreateMap<CreateProjectCommand, Project>();
            CreateMap<UpdateProjectCommand, Project>();
            CreateMap<PaginationResult<Project>, PaginationResult<ProjectViewModel>>();
        }

    }
}
