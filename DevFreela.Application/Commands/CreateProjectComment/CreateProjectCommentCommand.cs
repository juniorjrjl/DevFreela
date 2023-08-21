using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment
{
    public record CreateProjectCommentCommand(string Comment, int UserId, int? ProjectId) : IRequest<ProjectComment>;
    
}
