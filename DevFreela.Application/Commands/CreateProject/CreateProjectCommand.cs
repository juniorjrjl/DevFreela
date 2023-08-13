using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    
    public class CreateProjectCommand : IRequest<Project>
    {
        public string Title { get; private set; } 

        public string Description { get; private set; }

        public int ClientId { get; private set; }

        public int FreelancerId { get; private set; }

        public decimal TotalCost { get; private set; }
    }

}
