using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.model;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public record GetAllProjectsQuery(string? Query, int Page = 1) : IRequest<PaginationResult<Project>>;
