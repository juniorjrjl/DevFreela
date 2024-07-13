using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Mapper;

public interface IProjectCommentMapper
{
    ProjectComment ToEntity(CreateProjectCommentCommand command);
    
}