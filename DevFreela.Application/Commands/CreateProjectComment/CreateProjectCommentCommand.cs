using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment
{
    
    public class CreateProjectCommentCommand : IRequest<ProjectComment>
    {
        public int Id { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }
    }

}
