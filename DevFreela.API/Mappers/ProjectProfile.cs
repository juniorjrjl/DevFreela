using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers
{
    public class ProjectProfile : Profile
    {
        
        public ProjectProfile()
        {
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Project, ProjectDetailsViewModel>();
            CreateMap<NewProjectInputModel, CreateProjectCommand>();
            CreateMap<CreateProjectCommand, CreatedProjectViewModel>();
            CreateMap<UpdateProjectInputModel, CreateProjectCommand>();
            CreateMap<CreateProjectCommand, UpdatedProjectViewModel>();
            CreateMap<CreateCommentInputModel, ProjectComment>();
        }

    }
}