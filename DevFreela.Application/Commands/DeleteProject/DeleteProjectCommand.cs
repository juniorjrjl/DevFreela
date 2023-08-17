using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    
    public class DeleteProjectCommand : IRequest
    {

        public DeleteProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; } 
        
    }

}
