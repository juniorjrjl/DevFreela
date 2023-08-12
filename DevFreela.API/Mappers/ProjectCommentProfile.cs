using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers
{
    public class ProjectCommentProfile : Profile
    {

        public ProjectCommentProfile()
        {
            CreateMap<CreateCommentInputModel, ProjectComment>();
        }

    }
}