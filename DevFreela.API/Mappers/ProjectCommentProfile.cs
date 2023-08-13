using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers
{
    public class ProjectCommentProfile : Profile
    {

        public ProjectCommentProfile()
        {
            CreateMap<CreateCommentInputModel, CreateProjectCommentCommand>();
        }

    }
}