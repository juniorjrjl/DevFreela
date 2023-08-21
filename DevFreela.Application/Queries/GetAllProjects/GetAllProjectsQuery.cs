using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public record GetAllProjectsQuery(string Query) : IRequest<List<Project>>;
}
