using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public record GetProjectByIdQuery(int Id) : IRequest<Project>;
