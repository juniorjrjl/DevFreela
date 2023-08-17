using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    
    public class FinishProjectCommand : IRequest
    {

        public FinishProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; } 
        
    }

}
