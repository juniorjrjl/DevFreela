using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Mapper;

public class ProjectCommentMapper : IProjectCommentMapper
{
    public ProjectComment ToEntity(CreateProjectCommentCommand command) => 
        new 
        (
            command.Comment,
            command.ProjectId,
            command.UserId
        );
}