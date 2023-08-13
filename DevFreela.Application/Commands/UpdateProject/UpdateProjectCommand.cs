using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{
    
    public class UpdateProjectCommand : IRequest<Project>
    {
        public int Id { get; set; }
        public string Title { get; private set; } 

        public string Description { get; private set; }

        public decimal TotalCost { get; private set; }
        
    }

}
