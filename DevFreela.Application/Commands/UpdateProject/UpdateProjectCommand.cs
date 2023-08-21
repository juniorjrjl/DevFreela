using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{
    public record UpdateProjectCommand(string Title, string Description, decimal TotalCost, int? Id) : IRequest<Project>;

}
