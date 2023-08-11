using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers
{
    public class ProjectProfile : Profile
    {
        
        public ProjectProfile()
        {
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Project, ProjectDetailsViewModel>();
            CreateMap<NewProjectInputModel, Project>();
            CreateMap<Project, CreatedProjectViewModel>();
            CreateMap<UpdateProjectInputModel, Project>();
            CreateMap<Project, UpdatedProjectViewModel>();
            CreateMap<CreateCommentInputModel, ProjectComment>();
        }

    }
}