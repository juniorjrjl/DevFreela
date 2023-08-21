using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public record CreateProjectCommand(string Title, string Description, int ClientId, int FreelancerId, decimal TotalCost) : IRequest<Project>;

}
