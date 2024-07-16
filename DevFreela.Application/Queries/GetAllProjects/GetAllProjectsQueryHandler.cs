using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.model;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(IProjectQueryRepository projectQueryRepository) : IRequestHandler<GetAllProjectsQuery, PaginationResult<Project>>
{
    private readonly IProjectQueryRepository _projectQueryRepository = projectQueryRepository;

    public async Task<PaginationResult<Project>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken) => 
        await _projectQueryRepository.GetAllAsync(query.Query, query.Page);

}
